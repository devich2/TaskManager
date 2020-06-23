using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Entities.Tables.Identity
{
    public class Role : IdentityRole<int>
    {
        public decimal Rank { get; set; }
        public ICollection<Permission> Permissions {get;set;}
    }
}