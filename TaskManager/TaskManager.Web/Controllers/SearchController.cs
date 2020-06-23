using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Bll.Abstract.Search;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Result;

namespace TaskManager.Web.Controllers
{
    [Route("api/search/")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        
        [HttpGet]
        [Route("users")]
        public async Task<DataResult<List<UserInfoModel>>> GetUsers(string searchString)
        {
            return await _searchService.SearchByUsernameOrName(searchString);
        }
    }
}