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

        public async Task<DataResult<UnitAddResponse>> ProcessUnitCreate(UnitCreateOrUpdateModel model)
        {
            return await ProcessUnitCreateTransaction(model);
        }

        public async Task<DataResult<UnitUpdateResponse>> ProcessUnitUpdate(UnitCreateOrUpdateModel model)
        {
            return await ProcessUnitUpdateTransaction(model);
        }

        public async Task<Result> ProcessUnitDelete(int unitId)
        {
            return await ProcessUnitDeleteTransaction(unitId);
        }

        private async Task<Result> ProcessUnitDeleteTransaction(int unitId)
        {
            if (unitId < 0)
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
                var itemToDelete = await _unitOfWork.Units.GetByUnitIdAsync(unitId);
                if (itemToDelete != null)
                {
                    await _unitOfWork.Units.DeleteAsync(itemToDelete);
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
            await _unitOfWork.SaveAsync();
            
            methodResult.Data.UnitState = model.Status;
            methodResult.ResponseStatusType = ResponseStatusType.Succeed;
            methodResult.Data.UnitId = unitEntity.UnitId;
            return methodResult;
        }

        private async Task<DataResult<UnitUpdateResponse>> ProcessUnitUpdateTransaction(UnitCreateOrUpdateModel model)
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

                    unitEntity.Name = model.UnitStateModel.UnitModel.Name;
                    unitEntity.Description = model.UnitStateModel.UnitModel.Description;

                    var termInfo = model.TermInfo;

                    if (termInfo != null)
                    {
                        unitEntity.TermInfo.DueTs = termInfo.DueTs;
                        unitEntity.TermInfo.Status = termInfo.Status;
                    }

                    await _unitOfWork.Units.UpdateAsync(unitEntity);
                    await _unitOfWork.SaveAsync();

                    IUnitExtendedStrategy strategy =
                        _unitExtendedStrategyFactory.GetInstance(model.UnitStateModel.ExtendedType);

                    Result extendedProcessResult =
                        await strategy.ProcessExtendedItem
                            (model.UnitStateModel.Data, ModelState.Modify, unitEntity.UnitId);

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

        private async Task<DataResult<UnitAddResponse>> ProcessUnitCreateTransaction(UnitCreateOrUpdateModel model)
        {
            return await _transactionManager.ExecuteInTransactionAsync(async transaction =>
            {
                try
                {
                    Entities.Tables.Unit unitEntity = _mapper.Map<Entities.Tables.Unit>(model);

                    await _unitOfWork.Units.AddAsync(unitEntity);
                    await _unitOfWork.SaveAsync();

                    TermInfo term = new TermInfo();
                    if (model.TermInfo != null)
                    {
                        term = _mapper.Map<TermInfo>(model.TermInfo);
                        term.UnitId = unitEntity.UnitId;
                        term.StartTs = DateTimeOffset.Now;
                        await _unitOfWork.TermInfos.AddAsync(term);
                        await _unitOfWork.SaveAsync();
                    }

                    IUnitExtendedStrategy strategy =
                        _unitExtendedStrategyFactory.GetInstance(model.UnitStateModel.ExtendedType);

                    Result extendedProcessResult =
                        await strategy.ProcessExtendedItem
                            (model.UnitStateModel.Data, ModelState.Add, unitEntity.UnitId);

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