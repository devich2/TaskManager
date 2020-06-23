using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TaskManager.Entities.Enum;
using TaskManager.Models.Result;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base
{
    public interface IUnitExtendedStrategy
    {
        Task<Result> ProcessExtendedItem(JObject boxingItem, ModelState state, int unitId);
        Task<DataResult<JObject>> ProcessExistingModel(Entities.Tables.Unit unit);
        Task<bool> IsExisting(int unitId);
    }
}
