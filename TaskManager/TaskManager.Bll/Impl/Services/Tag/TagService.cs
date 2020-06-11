using System;
using TaskManager.Bll.Abstract.Tag;
using TaskManager.Dal.Abstract;
using TaskManager.Entities.Tables;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Impl.Services.Tag
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async System.Threading.Tasks.Task<Result> AddTag(int taskId, int tagId)
        {
            Result result = new Result();
            if (!await _unitOfWork.Tasks.IsExisting(taskId))
            {
                result.ResponseStatusType = ResponseStatusType.Error;
                result.Message = ResponseMessageType.InvalidId;
                result.MessageDetails = $"Task with id-{taskId} not found";
            }
            else if (!await _unitOfWork.Tags.IsExisting(tagId))
            {
                result.ResponseStatusType = ResponseStatusType.Error;
                result.Message = ResponseMessageType.InvalidId;
                result.MessageDetails = $"Tag with id-{tagId} not found";
            }
            else
            {
                await _unitOfWork.TagOnTasks.AddAsync(new TagOnTask()
                {
                    TagId = tagId,
                    TaskId = taskId
                });
                await _unitOfWork.SaveAsync();

                result.ResponseStatusType = ResponseStatusType.Succeed;
                result.Message = ResponseMessageType.None;
            }
            return result;
        }

        public async System.Threading.Tasks.Task<Result> RemoveTag(int taskId, int tagId)
        {
            Result result = new Result();
            TagOnTask tagOnTask =
                await _unitOfWork.TagOnTasks.FirstOrDefaultAsync(x => x.TagId == tagId && x.TaskId == taskId);
            if(tagOnTask == null)
            {
                result.ResponseStatusType = ResponseStatusType.Error;
                result.Message = ResponseMessageType.InvalidId;
                result.MessageDetails = $"Task id-{taskId} has no attached tag id-{tagId}";
            }
            else
            {
                await _unitOfWork.TagOnTasks.DeleteAsync(tagOnTask);
                result.ResponseStatusType = ResponseStatusType.Succeed;
                result.Message = ResponseMessageType.None;
            }
            return result;
        }
    }
}