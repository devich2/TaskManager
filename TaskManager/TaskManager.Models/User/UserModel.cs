using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Models.Role;

namespace TaskManager.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public HashSet<RoleModel> Roles { get; set; }
    }
}
