using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
using TaskManager.Dal.Abstract;
using TaskManager.Dal.Abstract.IRepository;
using TaskManager.Dal.Impl;
using TaskManager.Dal.Impl.ImplRepository;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables.Abstract;
using TaskManager.Models.Response;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy.Base
{
    public class BaseStrategy<TKey, TEntity, TModel>: IUnitExtendedStrategy
        where TEntity : class, IUnitExtensionTable
        where TModel : class
    {
        private readonly IUnitFkRepository<TEntity> _currentRepository;
        private readonly IMapper _mapper;
        private readonly JsonSerializerSettings _serializerSettings;
        public BaseStrategy(IUnitFkRepository<TEntity> currentRepository,
            IMapper mapper,
            IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
        {
            _currentRepository = currentRepository;
            _mapper = mapper;
            _serializerSettings = jsonOptions.Value.SerializerSettings;
        }


        public virtual async Task<DataResult<JObject>>
            ProcessExistingModel(Entities.Tables.Unit unit)
        {
            if (unit == null || unit.Id == 0)
            {
                return new DataResult<JObject>()
                {
                    Message = ResponseMessageType.InvalidModel,
                    ResponseStatusType = ResponseStatusType.Error
                };
            }
            TEntity data = await _currentRepository.GetByUnitIdAsync(unit.Id);

            if (data == null)
            {
                return new DataResult<JObject>()
                {
                    Message = ResponseMessageType.NotFound,
                    ResponseStatusType = ResponseStatusType.Warning
                };
            }

            //TModel model = _mapper.Map(data);

            return new DataResult<JObject>()
            {
                ResponseStatusType = ResponseStatusType.Succeed,
               // Data = JObject.FromObject(model)
            };
        }

        public virtual async Task<Result>
           ProcessExtendedItem(JObject boxingItem, ModelState state, int unitId)
        {
            TModel current =
                boxingItem.ToObject<TModel>(JsonSerializer.Create(_serializerSettings));

            if (current == null)
            {
                return new Result()
                {
                    Message = ResponseMessageType.InvalidModel,
                    ResponseStatusType = ResponseStatusType.Error
                };
            }

           /* TEntity entity = _modelMapper.MapBack(current);
            entity.UnitId = unitId;

            switch (state)
            {
                case ModelState.None:
                    break;
                case ModelState.Added:
                    await this.CreateAsync(entity);
                    break;
                case ModelState.Modify:
                    await this.UpdateAsync(entity);
                    break;
                case ModelState.Deleted:
                    await this.DeleteAsync(entity);
                    break;
            }

            await _currentRepository.SaveAsync();
            */
            return new Result()
            {
                Message = ResponseMessageType.None,
                ResponseStatusType = ResponseStatusType.Succeed
            };
            
        }

        protected virtual async Task CreateAsync(TEntity entity)
        {
            await _currentRepository.AddAsync(entity);
            await _currentRepository.SaveChangesAsync();
        }

        protected virtual async Task UpdateAsync(TEntity entity)
        {
            await _currentRepository.UpdateAsync(entity);
            await _currentRepository.SaveChangesAsync();
        }

        protected virtual async Task DeleteAsync(TEntity entity)
        {
            //TODO Soft delete 
            await _currentRepository.DeleteAsync(entity);
            await _currentRepository.SaveChangesAsync();
        }
    }
}
