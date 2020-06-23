using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Entities.Tables;
using TaskManager.Models.MileStone;

namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy
{
    public class MileStoneExtendedStrategy: BaseStrategy<Entities.Tables.MileStone, MileStoneCreateOrUpdateModel>, IUnitExtendedStrategy
    {
        public MileStoneExtendedStrategy(IUnitFkRepository<Entities.Tables.MileStone> currentRepository, IMapper mapper, IOptions<MvcNewtonsoftJsonOptions> jsonOptions) : base(currentRepository, mapper, jsonOptions)
        {
        }
    }
}