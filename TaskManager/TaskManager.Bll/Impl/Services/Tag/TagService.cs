using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Bll.Abstract.Tag;
using TaskManager.Dal.Abstract;
using TaskManager.Entities.Tables;
using TaskManager.Models.Result;
using TaskManager.Models.Tag;

namespace TaskManager.Bll.Impl.Services.Tag
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork,
        IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> UpdateTags(TagUpdateModel model)
        {
            Result result = new Result();
            if (!await _unitOfWork.Tasks.IsExisting(model.TaskId))
            {
                result.ResponseStatusType = ResponseStatusType.Error;
                result.Message = ResponseMessageType.InvalidId;
                result.MessageDetails = $"Task with id-{model.TaskId} not found";
            }
            else
            {
                List<string> tags = await _unitOfWork.TagOnTasks.GetTagsByTaskId(model.TaskId);
                List<string> tagsToRemove = tags.Except(model.Tags).ToList();
                List<string> tagsToAdd = model.Tags.Except(tags).ToList();
                
                if(tagsToRemove.Any())
                    await _unitOfWork.TagOnTasks.RemoveTags(model.TaskId, tagsToRemove);
                if(tagsToAdd.Any())
                    await _unitOfWork.TagOnTasks.AddToTags(model.TaskId, tagsToAdd);
                
                await _unitOfWork.SaveAsync();
                result.ResponseStatusType = ResponseStatusType.Succeed;
                result.Message = ResponseMessageType.None;
            }
            return result;
        }

        public async Task<DataResult<List<TagModel>>> GetTags()
        {
            DataResult<List<TagModel>> dataResult = new DataResult<List<TagModel>>();
            List<Entities.Tables.Tag> tags = await _unitOfWork.Tags.GetAllAsync();
            if(tags.Any())
            {
                dataResult.ResponseStatusType = ResponseStatusType.Succeed;
                dataResult.Message = ResponseMessageType.None;
                dataResult.Data = tags.Select(_mapper.Map<TagModel>).ToList();
                
            }
            else
            {
                dataResult.ResponseStatusType = ResponseStatusType.Warning;
                dataResult.Message = ResponseMessageType.EmptyResult;
            }
            return dataResult;
        }
        
        public async Task<DataResult<List<string>>> GetTagsByTaskId(int taskId)
        {
            DataResult<List<string>> dataResult = new DataResult<List<string>>();
            
            if(!await _unitOfWork.Tasks.IsExisting(taskId))
            {
                dataResult.ResponseStatusType = ResponseStatusType.Error;
                dataResult.Message = ResponseMessageType.InvalidId;
            }
            else
            {
                dataResult.ResponseStatusType = ResponseStatusType.Succeed;
                dataResult.Data = await _unitOfWork.TagOnTasks.GetTagsByTaskId(taskId);
            }
            return dataResult;
        }
    }
}