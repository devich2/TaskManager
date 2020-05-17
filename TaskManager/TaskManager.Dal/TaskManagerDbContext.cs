using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.NameTranslation;
using TaskManager.Entities.Enum;
using TaskManager.Entities.Tables;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Seed;

namespace TaskManager.Dal
{
    public class TaskManagerDbContext : IdentityDbContext<User, Role, int>
    {

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
            : base(options)
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Status>(
                nameof(Status),
                new NpgsqlNullNameTranslator()
            );
            NpgsqlConnection.GlobalTypeMapper.MapEnum<UnitType>(
                nameof(UnitType),
                new NpgsqlNullNameTranslator()
            );
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Enums

            builder.HasPostgresEnum(nameof(Status), typeof(Status).GetEnumNames());
            builder.HasPostgresEnum(nameof(UnitType), typeof(UnitType).GetEnumNames());
            #endregion

            #region Key

            builder.Entity<Permission>().HasKey(c => new { c.ProjectMemberId, c.RoleId });
            builder.Entity<Project>().HasKey(c => c.Id);
            builder.Entity<ProjectMember>().HasKey(c => c.Id);
            builder.Entity<RelationShip>().HasKey(c => new { c.UnitId, c.ParentUnitId });
            builder.Entity<Tag>().HasKey(c => c.Id);
            builder.Entity<TagOnTask>().HasKey(c => new { c.TagId, c.TaskId });
            builder.Entity<Task>().HasKey(c => c.Id);
            builder.Entity<Unit>().HasKey(c => c.UnitId);
            builder.Entity<TermInfo>().HasKey(c => c.UnitId);
            #endregion

            #region ValueGenerations

            builder.Entity<Project>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ProjectMember>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Tag>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Task>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            /*builder.Entity<TermInfo>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
                */

            builder.Entity<Unit>()
                .Property(p => p.UnitId)
                .ValueGeneratedOnAdd();

            #endregion

            #region RelationShips

            builder.Entity<Permission>()
                .HasOne(p => p.ProjectMember)
                .WithMany()
                .HasForeignKey(p => p.ProjectMemberId);

            builder.Entity<Task>()
                .HasOne(p => p.Unit)
                .WithOne()
                .HasForeignKey<Task>(p => p.UnitId);

            builder.Entity<Project>()
                .HasOne(p => p.Unit)
                .WithOne()
                .HasForeignKey<Project>(p => p.UnitId);

            builder.Entity<RelationShip>()
                .HasOne(p => p.Unit)
                .WithMany()
                .HasForeignKey(p => p.UnitId);

            builder.Entity<RelationShip>()
                .HasOne(p => p.ParentUnit)
                .WithMany(p => p.SubUnits)
                .HasForeignKey(p => p.ParentUnitId);

            builder.Entity<TagOnTask>()
                .HasOne(p => p.Task)
                .WithMany(p => p.TagOnTasks)
                .HasForeignKey(p => p.TaskId);

            builder.Entity<TagOnTask>()
                .HasOne(p => p.Tag)
                .WithMany(p => p.TagOnTasks)
                .HasForeignKey(p => p.TagId);

            builder.Entity<Unit>()
                .HasOne(p => p.TermInfo)
                .WithOne(p => p.Unit)
                .HasForeignKey<TermInfo>(p => p.UnitId);

            builder.Entity<Unit>()
                .HasOne(p => p.Creator)
                .WithMany()
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Unit>()
                .Property(p => p.Key)
                .HasDefaultValueSql("uuid_generate_v1()");

            #endregion

            DatabaseInitializer.SeedDatabase(builder);
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<RelationShip> RelationShips { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagOnTask> TagOnTasks { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TermInfo> TermInfos { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}
