using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Entities.Tables
{
    public class Permission
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int ProjectMemberId { get; set; }
        public ProjectMember ProjectMember { get; set; }
    }
}
