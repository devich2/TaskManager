namespace TaskManager.Models.Response
{
    public class ProjectMemberResponse: UserResponse
    {
        public string RoleName {get; set;}
        public int ProjectId {get; set;}
        public int RoleId {get; set;}
    }
}