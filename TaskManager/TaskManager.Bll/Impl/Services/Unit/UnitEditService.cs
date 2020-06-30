using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Common.Utils;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.Transactions;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;
using Task = TaskManager.Entities.Tables.Task;

namespace TaskManager.Bll.Impl.Services.Unit
{
    public class UnitEditService : IUnitEditService
    {
        private readonly ITransactionManager _transactionManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitExtendedStrategyFactory _unitExtendedStrategyFactory;
        private readonly ILogger<UnitEditService> _logger;

        public UnitEditService(ITransactionManager transactionManager,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUnitExtendedStrategyFactory unitExtendedStrategyFactory,
            ILogger<UnitEditService> logger)
        {
            _transactionManager = transactionManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _unitExtendedStrategyFactory = unitExtendedStrategyFactory;
            _logger = logger;
        }

        public async Task<DataResult<UnitAddResponse>> ProcessUnitCreate(UnitBlModel model)
        {
            return await ProcessUnitCreateTransaction(model);
        }

        public async Task<DataResult<UnitUpdateResponse>> ProcessUnitUpdate(UnitBlModel model)
        {
            return await ProcessUnitUpdateTransaction(model);
        }

        public async Task<Result> ProcessUnitDelete(UnitDeleteModel model)
        {
            return await ProcessUnitDeleteTransaction(model);
        }

        private async Task<Result> ProcessUnitDeleteTransaction(UnitDeleteModel model)
        {
            if (model.UnitId < 0)
            {
                return new Result
                {
                    Message = ResponseMessageType.InvalidId,
                    ResponseStatusType = ResponseStatusType.Error,
                    MessageDetails = "id could not be less or equal to 0"
                };
            }

            return await _transactionManager.ExecuteInImplicitTransactionAsync(async () =>
            {
                var itemToDelete = await _unitOfWork.Units
                    .FirstOrDefaultAsync(x=>x.UnitId == model.UnitId && x.UnitType == model.UnitType);
                    
                if (itemToDelete != null)
                {
                    await _unitOfWork.Units.DeleteAsync(itemToDelete);
                    await _unitOfWork.SaveAsync();
                    return new Result
                    {
                        ResponseStatusType = ResponseStatusType.Succeed
                    };
                }

                return new Result
                {
                    Message = ResponseMessageType.NotFound,
                    ResponseStatusType = ResponseStatusType.Error,
                    MessageDetails = "Could not find menu item with given id"
                };
            });
        }

        public async Task<DataResult<UnitUpdateResponse>> ProcessContentChangeStatus(UnitStatusPatchModel model)
        {
            DataResult<UnitUpdateResponse> methodResult =
                new DataResult<UnitUpdateResponse>();

            Entities.Tables.Unit unitEntity =
                await _unitOfWork.Units.GetByUnitIdAsync(model.UnitId);

            if (unitEntity == null)
            {
                methodResult.ResponseStatusType = ResponseStatusType.Error;
                methodResult.Message = ResponseMessageType.NotFound;
                methodResult.MessageDetails = "Unit not found";
                return methodResult;
            }

            IUnitExtendedStrategy strategy =
                _unitExtendedStrategyFactory.GetInstance(unitEntity.UnitType);
            bool existExtendedTable = await strategy.IsExisting(unitEntity.UnitId);

            if (!existExtendedTable)
            {
                methodResult.ResponseStatusType = ResponseStatusType.Error;
                methodResult.Message = ResponseMessageType.InvalidModel;
                methodResult.MessageDetails = "Extended table doesnt exist";
                return methodResult;
            }

            unitEntity.TermInfo.Status = model.Status;
            await _unitOfWork.Units.UpdateAsync(unitEntity);
            await _unitOfWork.SaveAsync();
            
            methodResult.ResponseStatusType = ResponseStatusType.Succeed;
            methodResult.Data = new UnitUpdateResponse()
            {
                UnitId = unitEntity.UnitId,
                UnitState = model.Status
            };
            return methodResult;
        }

        private async Task<DataResult<UnitUpdateResponse>> ProcessUnitUpdateTransaction(UnitBlModel model)
        {
            return await _transactionManager.ExecuteInTransactionAsync(async transaction =>
            {
                try
                {
                    Entities.Tables.Unit unitEntity =
                        await _unitOfWork.Units.GetByUnitIdAsync(model.UnitId);

                    if (unitEntity == null)
                    {
                        return new DataResult<UnitUpdateResponse>()
                        {
                            ResponseStatusType = ResponseStatusType.Error,
                            Message = ResponseMessageType.IdIsMissing
                        };
                    }

                    unitEntity.Name = model.Name;
                    unitEntity.Description = model.Description;

                    var termInfo = model.TermInfo;

                    if (termInfo != null)
                    {
                        unitEntity.TermInfo.DueTs = termInfo.DueTs;
                        unitEntity.TermInfo.Status = termInfo.Status;
                    }

                    await _unitOfWork.Units.UpdateAsync(unitEntity);
                    await _unitOfWork.SaveAsync();

                    IUnitExtendedStrategy strategy =
                        _unitExtendedStrategyFactory.GetInstance(model.ExtendedType);

                    Result extendedProcessResult =
                        await strategy.ProcessExtendedItem
                            (model.Data, ModelState.Modify, unitEntity.UnitId);

                    if (extendedProcessResult.ResponseStatusType == ResponseStatusType.Succeed)
                    {
                        await transaction.CommitAsync();
                        return new DataResult<UnitUpdateResponse>()
                        {
                            Data = new UnitUpdateResponse()
                            {
                                UnitId = unitEntity.UnitId,
                                UnitState = unitEntity.TermInfo.Status
                            }
                        };
                    }

                    await transaction.RollbackAsync();
                    return new DataResult<UnitUpdateResponse>()
                    {
                        Data = new UnitUpdateResponse()
                        {
                            UnitState = Status.None
                        },
                        Message = extendedProcessResult.Message,
                        ResponseStatusType = extendedProcessResult.ResponseStatusType
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogW(e, "ContentAdminBlModel: {C}", args:
                        new object[]
                        {
                            AsJsonFormatter.Create(model)
                        });

                    return new DataResult<UnitUpdateResponse>()
                    {
                        Data = new UnitUpdateResponse()
                        {
                            UnitState = Status.None
                        },
                        Message = ResponseMessageType.InvalidModel,
                        ResponseStatusType = ResponseStatusType.Error
                    };
                }
            });
        }

        private async Task<DataResult<UnitAddResponse>> ProcessUnitCreateTransaction(UnitBlModel model)
        {
            return await _transactionManager.ExecuteInTransactionAsync(async transaction =>
            {
                try
                {
                    Entities.Tables.Unit unitEntity = _mapper.Map<Entities.Tables.Unit>(model);
                    unitEntity.CreatorId = model.UserId;
                    await _unitOfWork.Units.AddAsync(unitEntity);
                    await _unitOfWork.SaveAsync();
                    
                    if (model.TermInfo == null)
                    {
                        await transaction.RollbackAsync();
                        return new DataResult<UnitAddResponse>()
                        {
                            Data = new UnitAddResponse()
                            {
                                UnitState = Status.None
                            },
                            Message = ResponseMessageType.TermInfoMissing,
                            ResponseStatusType = ResponseStatusType.Error
                        };
                    }

                    TermInfo term = _mapper.Map<TermInfo>(model.TermInfo);
                    term.UnitId = unitEntity.UnitId;
                    term.StartTs = DateTimeOffset.Now;
                    await _unitOfWork.TermInfos.AddAsync(term);
                    await _unitOfWork.SaveAsync();

                    IUnitExtendedStrategy strategy =
                        _unitExtendedStrategyFactory.GetInstance(model.ExtendedType);

                    Result extendedProcessResult =
                        await strategy.ProcessExtendedItem
                            (model.Data, ModelState.Add, unitEntity.UnitId);

                    if (extendedProcessResult.ResponseStatusType == ResponseStatusType.Succeed)
                    {
                        await transaction.CommitAsync();
                        return new DataResult<UnitAddResponse>()
                        {
                            Data = new UnitAddResponse()
                            {
                                UnitId = unitEntity.UnitId,
                                UnitState = term.Status
                            }
                        };
                    }

                    await transaction.RollbackAsync();
                    return new DataResult<UnitAddResponse>()
                    {
                        Data = new UnitAddResponse()
                        {
                            UnitState = Status.None
                        },
                        Message = extendedProcessResult.Message,
                        ResponseStatusType = extendedProcessResult.ResponseStatusType
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogW(e, "UnitCreateOrUpdateModel: {C}", args:
                        new object[]
                        {
                            AsJsonFormatter.Create(model)
                        });

                    return new DataResult<UnitAddResponse>()
                    {
                        Message = ResponseMessageType.InvalidModel,
                        ResponseStatusType = ResponseStatusType.Error
                    };
                }
            });
        }
    }
}