using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Seed;

namespace TaskManager.Dal
{
    public class TaskManagerDbContext : IdentityDbContext<User, Role, int>
    {

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasPostgresEnum(typeof(Language).Name, typeof(Language).GetEnumNames());
            builder.HasPostgresEnum(typeof(Status).Name, typeof(Status).GetEnumNames());
            builder.HasPostgresEnum(typeof(ContentType).Name, typeof(ContentType).GetEnumNames());
            DatabaseInitializer.SeedDatabase(builder);
        }
    }
}
