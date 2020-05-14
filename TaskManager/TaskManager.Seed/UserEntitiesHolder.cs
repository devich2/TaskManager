using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Seed
{
    public class UserEntitiesHolder
    {
        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "David",
                UserName="@devich",
                Email ="devidshylyuk85@gmail.com",
                NormalizedUserName ="David".ToUpper(),
                NormalizedEmail ="devidshylyuk85@gmail.com".ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAEJMqefM3jQQE7sOvJCM73AKmMaFQqF0t01IbCdmU+x7KcgHlBoETO6+XXtvJ+wB9UA==",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78006aefd74b",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = 2,
                Name = "Ola",
                UserName ="@olga",
                Email ="olarevun23@gmail.com",
                NormalizedUserName = "Ola".ToUpper(),
                NormalizedEmail ="olarevun23@gmail.com".ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAELhW7WoGTkP1aZcDoN5qwgHILFMMak47gnjEKYQ0YBgcEitvLKiKmpoXYliqdFfMVA==",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78006aefd74b",
                TwoFactorEnabled = false
            },
            new User
            {
                Id = 3,
                Name = "Oleg",
                UserName ="@olegka",
                Email ="olegrevun23@gmail.com",
                NormalizedUserName = "Oleg".ToUpper(),
                NormalizedEmail ="olegrevun23@gmail.com".ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAELhW7WoGTkP1aZcDoN5qwgHILFMMak47gnjEKYQ0YBgcEitvLKiKmpoXYliqdFfMVA==",
                ConcurrencyStamp = "cda9194a-63f5-4643-afdd-78006aefd74b",
                TwoFactorEnabled = false
            }
        };

        public List<User> GetUsers()
        {
            return _users;
        }
    }
}
