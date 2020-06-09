using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Models.Response;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy
{
    public class NoUnitExtendedStrategy: IUnitExtendedStrategy
    {
        private JsonSerializerSettings _serializerSettings;

        public NoUnitExtendedStrategy(IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
        {
            _serializerSettings = jsonOptions.Value.SerializerSettings;
        }

        public  Task<Result> ProcessExtendedItem(JObject boxingItem, ModelState state, int contentId)
        {
            return Task.FromResult(new Result()
            {
                Message = ResponseMessageType.None,
                ResponseStatusType = ResponseStatusType.Succeed
            });
        }

        public Task<DataResult<JObject>> ProcessExistingModel(Entities.Tables.Unit unit)
        {
            return Task.FromResult(new DataResult<JObject>()
            {
                Message = ResponseMessageType.None,
                ResponseStatusType = ResponseStatusType.Succeed
            });
        }
        
        public Task<bool> IsExisting(int unitId)
        {
            return Task.FromResult(true);
        }
    }
}