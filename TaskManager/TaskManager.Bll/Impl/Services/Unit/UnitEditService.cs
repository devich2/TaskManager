using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaskManager.Bll.Abstract.Unit;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Common.Utils;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.Transactions;
using TaskManager.Entities.Enum;
using TaskManager.Models.Response;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Impl.Services.Unit
{
    public class UnitEditService: IUnitEditService
    {
        private readonly ITransactionManager _transactionManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitExtendedStrategyFactory _unitExtendedStrategyFactory;
        private readonly ILogger<UnitEditService> _logger;

        public UnitEditService(ITransactionManager transactionManager,
            IUnitOfWork unitOfWork,
            IUnitExtendedStrategyFactory unitExtendedStrategyFactory,
            ILogger<UnitEditService> logger)
        {
            _transactionManager = transactionManager;
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

        private async Task<DataResult<UnitUpdateResponse>> ProcessUnitUpdateTransaction(UnitCreateOrUpdateModel model)
        {
            return await _transactionManager.ExecuteInTransactionAsync(transaction =>
            {
                try
                {

                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogW(e, "ContentAdminBlModel: {C}", args:
                        new object[]
                        {
                            AsJsonFormatter.Create(model)
                        });

                    return new DataResult<ContentUpdateResponse>()
                    {
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
                    Entities.Tables.Unit unitEntity =
                        new Entities.Tables.Unit()
                        {
                            UnitType = model.UnitStateModel.ExtendedType,
                            CreatorId = model.UserId,
                            Name = model.UnitStateModel.UnitModel.Name,
                            Description = model.UnitStateModel.UnitModel.Description
                        };

                    await _unitOfWork.Units.AddAsync(unitEntity);
                    await _unitOfWork.SaveAsync();

 
                    IUnitExtendedStrategy strategy =
                        _unitExtendedStrategyFactory.GetInstance(model.UnitStateModel.ExtendedType);

                    Result extendedProcessResult =
                        await strategy.ProcessExtendedItem
                            (model.UnitStateModel.Data, ModelState.Added, unitEntity.UnitId);

                    if (extendedProcessResult.ResponseStatusType == ResponseStatusType.Succeed)
                    {
                        await transaction.CommitAsync();
                        return new DataResult<UnitAddResponse>()
                        {
                            Data = new UnitAddResponse()
                            {
                                UnitId = unitEntity.UnitId,
                                UnitState = Status.Open
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
