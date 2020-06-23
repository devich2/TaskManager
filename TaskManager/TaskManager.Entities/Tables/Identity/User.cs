using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Entities.Tables.Identity
{
    public class User: IdentityUser<int>, IEquatable<User>
    {
        public string Name { get; set; }
        public ICollection<ProjectMember> UserProjects {get;set;}
        
        public DateTimeOffset LastLoginDate { get; set; }
        
        public DateTime? RegistrationDate { get; set; }
        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
