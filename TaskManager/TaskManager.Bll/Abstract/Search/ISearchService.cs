using System.Collections.Generic;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Abstract.Search
{
    public interface ISearchService
    {
        System.Threading.Tasks.Task<DataResult<List<UserInfoModel>>> SearchByUsernameOrName(string searchString);
    }
}