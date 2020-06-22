using System.Collections.Generic;

namespace TaskManager.Models.ProjectMember
{
    public class ProjectMemberSelectionModel
    {
        public List<UserInfoModel> MemberList { get; set; }
    }

    public class UserInfoModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}