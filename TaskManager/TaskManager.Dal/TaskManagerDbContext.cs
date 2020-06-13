using System.ComponentModel.DataAnnotations.Schema;
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
            NpgsqlConnection.GlobalTypeMapper.MapEnum<PermissionType>(
                nameof(PermissionType),
                new NpgsqlNullNameTranslator()
            );
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Enums

            builder.HasPostgresEnum(nameof(Status), typeof(Status).GetEnumNames());
            builder.HasPostgresEnum(nameof(UnitType), typeof(UnitType).GetEnumNames());
            builder.HasPostgresEnum(nameof(PermissionType), typeof(PermissionType).GetEnumNames());
            #endregion

            #region Key
            
            builder.Entity<Project>().HasKey(c => c.UnitId);
            builder.Entity<Tag>().HasKey(c => c.Id);
            builder.Entity<TagOnTask>().HasKey(c => new { c.TagId, c.TaskId });
            builder.Entity<Task>().HasKey(c => c.Id);
            builder.Entity<Unit>().HasKey(c => c.UnitId);
            builder.Entity<TermInfo>().HasKey(c => c.UnitId);
            builder.Entity<Permission>().HasKey(c=>c.Id);
            builder.Entity<MileStone>().HasKey(c=>c.Id);
            builder.Entity<ProjectMember>().HasKey(c=>new{c.UserId, c.ProjectId});
            #endregion

            #region ValueGenerations
            
            builder.Entity<Tag>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Task>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            
            builder.Entity<Permission>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<MileStone>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            #endregion

            #region RelationShips
            
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
                .HasOne(p=>p.UnitParent)
                .WithMany(p=>p.Children)
                .HasForeignKey(p=>p.UnitParentId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Unit>()
                .Property(p => p.Key)
                .HasDefaultValueSql("uuid_generate_v1()");
            
            builder.Entity<Permission>()
                .HasOne(p=>p.Role)
                .WithMany(p=>p.Permissions)
                .HasForeignKey(p=>p.RoleId);
                
            builder.Entity<Task>()
                .HasOne(p=>p.MileStone)
                .WithMany(p=>p.Tasks)
                .HasForeignKey(p=>p.MileStoneId);
                
            builder.Entity<ProjectMember>()
                .HasOne(p=> p.Project)
                .WithMany(p=>p.ProjectMembers)
                .HasForeignKey(p=>p.ProjectId);
                
            builder.Entity<ProjectMember>()
                .HasOne(p=> p.User)
                .WithMany(p=>p.UserProjects)
                .HasForeignKey(p=>p.UserId);
            #endregion
            DatabaseInitializer.SeedDatabase(builder);
        }
        
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<ProjectMember> ProjectMembers{ get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagOnTask> TagOnTasks { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TermInfo> TermInfos { get; set; }
        public DbSet<Permission> Permissions {get;set;}
        public DbSet<MileStone> MileStones{get;set;}
        public DbSet<Unit> Units { get; set; }
    }
}
