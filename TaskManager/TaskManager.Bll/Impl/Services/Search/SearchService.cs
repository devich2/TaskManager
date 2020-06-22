using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Bll.Abstract.Search;
using TaskManager.Dal.Abstract;
using TaskManager.Models.ProjectMember;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Impl.Services.Search
{
    public class SearchService: ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchService(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<DataResult<List<UserInfoModel>>> SearchByUsernameOrName(string searchString)
        {
            DataResult<List<UserInfoModel>> dataResult = new DataResult<List<UserInfoModel>>();
            List<Entities.Tables.Identity.User> users = await _unitOfWork.ProjectMembers.GetMembersListByProjectId(null, searchString);
            if(users.Any())
            {
                dataResult.ResponseStatusType = ResponseStatusType.Succeed;
                dataResult.Data = users.Select(_mapper.Map<UserInfoModel>).ToList();
            }
            else
            {
                dataResult.ResponseStatusType = ResponseStatusType.Warning;
                dataResult.Message = ResponseMessageType.EmptyResult;
            }
            return dataResult;
        }
    }
}