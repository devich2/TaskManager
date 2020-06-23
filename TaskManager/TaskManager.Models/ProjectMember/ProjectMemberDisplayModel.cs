using TaskManager.Models.Role;
using TaskManager.Models.User;

namespace TaskManager.Models.ProjectMember
{
    public class ProjectMemberDisplayModel: UserBaseModel
    {
        public RoleRankModel Role {get; set;}
        public ProjectMemberPersonalModel Personal {get;set;}
    }
}