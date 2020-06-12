using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.Tag;
using TaskManager.Models.Response;
using TaskManager.Models.Result;
using TaskManager.Models.Tag;

namespace TaskManager.Web.Controllers
{
    [Route("api/{projectId}/tag/")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet]
        public async Task<DataResult<List<TagModel>>> Get()
        {
            return await _tagService.GetTags();
        }
        
        [HttpGet]
        [Route("{taskId}")]
        public async Task<Result> Get(int taskId)
        {
            return await _tagService.GetTagsByTaskId(taskId);
        }
        
        [HttpPut]
        [Route("{taskId}")]
        public async Task<Result> Put(int taskId, [FromBody] TagUpdateModel model)
        {
            model.TaskId = taskId;
            return await _tagService.UpdateTags(model);
        }
    }
}