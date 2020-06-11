using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities.Tables.Identity;

namespace TaskManager.Seed
{
    public static class DatabaseInitializer
    {
        public static void SeedDatabase(ModelBuilder builder)
        {
            // Seed "unit" entities
            var unitEntitiesHolder = new UnitEntitiesHolder();
            AddEntities(builder, unitEntitiesHolder.GetUnits());
            AddEntities(builder, unitEntitiesHolder.GeTermInfos());
            AddEntities(builder, unitEntitiesHolder.GeTags());
            AddEntities(builder, unitEntitiesHolder.GetProjects());
            AddEntities(builder, unitEntitiesHolder.GetMileStones());
            AddEntities(builder, unitEntitiesHolder.GeTasks());
            AddEntities(builder, unitEntitiesHolder.GeTagOnTasks());

            // Seed roles, users and permissions
            var rolesEntitiesHolder = new RolesEntitiesHolder();
            AddEntities(builder, rolesEntitiesHolder.GetRoles());

            var usersEntitiesHolder = new UserEntitiesHolder();
            var users = usersEntitiesHolder.GetUsers();

            PasswordHasher<User> ph = new PasswordHasher<User>();

            var newPassword = "1qaz2wsx";
            foreach (var user in users)
            {
                user.SecurityStamp = "9819F4B5-F389-4603-BF0B-1E3C88379627";
                if (user.PasswordHash == null ||
                    ph.VerifyHashedPassword(user, user.PasswordHash, newPassword)
                    == PasswordVerificationResult.Failed)
                {
                    user.PasswordHash = ph.HashPassword(user, newPassword);
                }
            }

            AddEntities(builder, users);

            var userClaimsHolder = new UserClaimsHolder();
            AddEntities(builder, userClaimsHolder.GetUserClaims());
            
            var permissionEntitiesHolder = new PermissionEntitiesHolder();
            AddEntities(builder, permissionEntitiesHolder.GetPermissions());
        }
        private static void AddEntities<T>(ModelBuilder builder, List<T> entities) where T : class
        {
            builder.Entity<T>().HasData(entities);
        }
    }
}
