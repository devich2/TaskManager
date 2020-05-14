using Microsoft.AspNetCore.Identity;

namespace TaskManager.Entities.Tables.Identity
{
    public class User: IdentityUser<int>
    {
        public string Name { get; set; }
    }
}
