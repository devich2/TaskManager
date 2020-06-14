using TaskManager.Models.User;

namespace TaskManager.Models.ProjectMember
{
    public class ProjectMemberDisplayModel: UserModel
    {
        public ProjectMemberPersonalModel Personal {get;set;}
    }
}