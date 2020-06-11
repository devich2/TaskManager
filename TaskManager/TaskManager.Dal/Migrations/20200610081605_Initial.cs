using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskManager.Dal.Migrations.InitialScript;
using TaskManager.Entities.Enum;

namespace TaskManager.Dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Resource.script_up);
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:PermissionType", "ProjectModify,ProjectDelete,MilestoneAdd,MilestoneModify,MilestoneDelete,SubTaskAdd,SubTaskModify,SubTaskDelete,TaskAdd,TaskModify,TaskDelete,CommentAdd,CommentModify,CommentDelete,RoleChange,StatusChange,UserInvite,UserKick,TagAdd,TagDelete,Read")
                .Annotation("Npgsql:Enum:Status", "None,Open,InProgress,Closed")
                .Annotation("Npgsql:Enum:UnitType", "Comment,Milestone,Project,Task,SubTask");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Rank = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TextValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(nullable: false),
                    PermissionType = table.Column<PermissionType>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MileStoneId = table.Column<int>(nullable: true),
                    AssignedId = table.Column<int>(nullable: true),
                    UnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_AssignedId",
                        column: x => x.AssignedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagOnTasks",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagOnTasks", x => new { x.TagId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TagOnTasks_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagOnTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UnitType = table.Column<UnitType>(nullable: false),
                    Key = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v1()"),
                    CreatorId = table.Column<int>(nullable: false),
                    ProjectUnitId = table.Column<int>(nullable: true),
                    UnitParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_Units_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Units_Units_UnitParentId",
                        column: x => x.UnitParentId,
                        principalTable: "Units",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MileStones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MileStones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MileStones_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    UnitId = table.Column<int>(nullable: false),
                    Members = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_Projects_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TermInfos",
                columns: table => new
                {
                    UnitId = table.Column<int>(nullable: false),
                    StartTs = table.Column<DateTimeOffset>(nullable: false),
                    DueTs = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<Status>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermInfos", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_TermInfos_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Rank" },
                values: new object[,]
                {
                    { 1, "cda9194a-63f5-4643-afdd-78006aefd74b", "Guest", "GUEST", 1m },
                    { 2, "cda9194a-63f5-4643-afdd-78006aefd74b", "Developer", "DEVELOPER", 2m },
                    { 3, "cda9194a-63f5-4643-afdd-78406aefd74b", "Maintainer", "MAINTAINER", 3m },
                    { 4, "cda9194a-63f5-4643-afdd-78406aefd74b", "Owner", "OWNER", 4m }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "cda9194a-63f5-4643-afdd-78006aefd74b", "devidshylyuk85@gmail.com", false, false, null, "David", "DEVIDSHYLYUK85@GMAIL.COM", "DAVID", "AQAAAAEAACcQAAAAEJMqefM3jQQE7sOvJCM73AKmMaFQqF0t01IbCdmU+x7KcgHlBoETO6+XXtvJ+wB9UA==", null, false, "9819F4B5-F389-4603-BF0B-1E3C88379627", false, "@devich" },
                    { 2, 0, "cda9194a-63f5-4643-afdd-78006aefd74b", "olarevun23@gmail.com", false, false, null, "Ola", "OLAREVUN23@GMAIL.COM", "OLA", "AQAAAAEAACcQAAAAELhW7WoGTkP1aZcDoN5qwgHILFMMak47gnjEKYQ0YBgcEitvLKiKmpoXYliqdFfMVA==", null, false, "9819F4B5-F389-4603-BF0B-1E3C88379627", false, "@olga" },
                    { 3, 0, "cda9194a-63f5-4643-afdd-78006aefd74b", "olegrevun23@gmail.com", false, false, null, "Oleg", "OLEGREVUN23@GMAIL.COM", "OLEG", "AQAAAAEAACcQAAAAELhW7WoGTkP1aZcDoN5qwgHILFMMak47gnjEKYQ0YBgcEitvLKiKmpoXYliqdFfMVA==", null, false, "9819F4B5-F389-4603-BF0B-1E3C88379627", false, "@olegka" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "TextValue" },
                values: new object[,]
                {
                    { 8, "Backend" },
                    { 7, "Frontend" },
                    { 6, "CodeReview" },
                    { 5, "Blocked" },
                    { 2, "Self-test" },
                    { 3, "Adminka" },
                    { 9, "Backlog" },
                    { 1, "InProgress" },
                    { 4, "Done" },
                    { 10, "Testing" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 3, "role", "Maintainer_1", 1 },
                    { 2, "role", "Guest_1", 1 },
                    { 1, "role", "Admin_1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "PermissionType", "RoleId" },
                values: new object[,]
                {
                    { 27, PermissionType.CommentDelete, 4 },
                    { 28, PermissionType.CommentModify, 4 },
                    { 29, PermissionType.MilestoneAdd, 4 },
                    { 30, PermissionType.MilestoneDelete, 4 },
                    { 31, PermissionType.MilestoneModify, 4 },
                    { 32, PermissionType.TaskAdd, 4 },
                    { 33, PermissionType.TaskDelete, 4 },
                    { 34, PermissionType.TaskModify, 4 },
                    { 35, PermissionType.SubTaskAdd, 4 },
                    { 37, PermissionType.SubTaskModify, 4 },
                    { 26, PermissionType.CommentAdd, 4 },
                    { 38, PermissionType.ProjectDelete, 4 },
                    { 39, PermissionType.ProjectModify, 4 },
                    { 40, PermissionType.RoleChange, 4 },
                    { 41, PermissionType.UserInvite, 4 },
                    { 42, PermissionType.UserKick, 4 },
                    { 43, PermissionType.TagAdd, 4 },
                    { 44, PermissionType.TagDelete, 4 },
                    { 45, PermissionType.StatusChange, 4 },
                    { 36, PermissionType.SubTaskDelete, 4 },
                    { 1, PermissionType.Read, 1 },
                    { 25, PermissionType.Read, 4 },
                    { 23, PermissionType.TagDelete, 3 },
                    { 2, PermissionType.Read, 2 },
                    { 3, PermissionType.CommentAdd, 2 },
                    { 4, PermissionType.CommentDelete, 2 },
                    { 5, PermissionType.CommentModify, 2 },
                    { 6, PermissionType.TagAdd, 2 },
                    { 7, PermissionType.TagDelete, 2 },
                    { 8, PermissionType.StatusChange, 2 },
                    { 9, PermissionType.Read, 3 },
                    { 10, PermissionType.CommentAdd, 3 },
                    { 11, PermissionType.CommentDelete, 3 },
                    { 12, PermissionType.CommentModify, 3 },
                    { 13, PermissionType.MilestoneAdd, 3 },
                    { 14, PermissionType.MilestoneDelete, 3 },
                    { 15, PermissionType.MilestoneModify, 3 },
                    { 16, PermissionType.TaskAdd, 3 },
                    { 17, PermissionType.TaskDelete, 3 },
                    { 18, PermissionType.TaskModify, 3 },
                    { 19, PermissionType.SubTaskAdd, 3 },
                    { 20, PermissionType.SubTaskDelete, 3 },
                    { 21, PermissionType.SubTaskModify, 3 },
                    { 22, PermissionType.TagAdd, 3 },
                    { 24, PermissionType.StatusChange, 3 }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "CreatorId", "Description", "Key", "Name", "ProjectUnitId", "UnitParentId", "UnitType" },
                values: new object[] { 20, 1, "Система отслеживания заданий. Выдача задания менеджером. Статус задания, согласно рабочему процессу. Процент выполнения. Почтовые уведомления клиентам системы. Управление пользователями и их ролями.", new Guid("bff26a36-6cb5-4cef-a7c4-939f6eaf76ca"), "TaskManager", null, null, UnitType.Project });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "UnitId", "Members" },
                values: new object[] { 20, 1 });

            migrationBuilder.InsertData(
                table: "TermInfos",
                columns: new[] { "UnitId", "DueTs", "StartTs", "Status" },
                values: new object[] { 20, new DateTimeOffset(new DateTime(2020, 6, 23, 12, 40, 40, 0, DateTimeKind.Unspecified), new TimeSpan(0, -2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 548, DateTimeKind.Unspecified).AddTicks(8027), new TimeSpan(0, 3, 0, 0, 0)), Status.InProgress });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "CreatorId", "Description", "Key", "Name", "ProjectUnitId", "UnitParentId", "UnitType" },
                values: new object[,]
                {
                    { 1, 2, "Create user api, spam list and blocking users", new Guid("dea2c6f6-3064-40fb-9f75-8e695939e839"), "CRUD API creating and deleting users", null, 20, UnitType.Task },
                    { 2, 2, "Modify database, add email service for client sales", new Guid("814d9772-ef7c-4eb9-a932-18dc89d4a0b4"), "Api for email subscriptions", null, 20, UnitType.Task },
                    { 3, 2, "Change behaviour from delete cascade to restrict and rework service deleting logic", new Guid("a7d245d0-3280-4ef5-9acb-6787bc194db7"), "Delete cascade", null, 20, UnitType.Task },
                    { 4, 2, "Plug in PayPal", new Guid("90992949-51c7-4ad1-aa92-086a1c57ba5d"), "Api for donations", null, 20, UnitType.Task },
                    { 5, 2, "Config docker compose with dotnet and postgres image and write integration tests for content with", new Guid("3310e655-5b08-493c-972c-13f668b5c57e"), "Testing content", null, 20, UnitType.Task },
                    { 50, 1, null, new Guid("2e5bc155-4842-4bf3-94de-36194204d917"), "MileStone1", null, 20, UnitType.Milestone }
                });

            migrationBuilder.InsertData(
                table: "MileStones",
                columns: new[] { "Id", "UnitId" },
                values: new object[] { 1, 50 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssignedId", "MileStoneId", "UnitId" },
                values: new object[,]
                {
                    { 1, null, null, 1 },
                    { 2, 1, null, 2 },
                    { 3, 1, null, 3 }
                });

            migrationBuilder.InsertData(
                table: "TermInfos",
                columns: new[] { "UnitId", "DueTs", "StartTs", "Status" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2020, 5, 25, 12, 40, 40, 0, DateTimeKind.Unspecified), new TimeSpan(0, -2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 545, DateTimeKind.Unspecified).AddTicks(7695), new TimeSpan(0, 3, 0, 0, 0)), Status.Open },
                    { 2, new DateTimeOffset(new DateTime(2020, 5, 30, 12, 40, 40, 0, DateTimeKind.Unspecified), new TimeSpan(0, -2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 548, DateTimeKind.Unspecified).AddTicks(7899), new TimeSpan(0, 3, 0, 0, 0)), Status.InProgress },
                    { 3, new DateTimeOffset(new DateTime(2020, 5, 27, 12, 40, 40, 0, DateTimeKind.Unspecified), new TimeSpan(0, -2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 548, DateTimeKind.Unspecified).AddTicks(7992), new TimeSpan(0, 3, 0, 0, 0)), Status.InProgress },
                    { 4, new DateTimeOffset(new DateTime(2020, 5, 26, 12, 40, 40, 0, DateTimeKind.Unspecified), new TimeSpan(0, -2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 548, DateTimeKind.Unspecified).AddTicks(8004), new TimeSpan(0, 3, 0, 0, 0)), Status.InProgress },
                    { 5, new DateTimeOffset(new DateTime(2020, 5, 23, 12, 40, 40, 0, DateTimeKind.Unspecified), new TimeSpan(0, -2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 548, DateTimeKind.Unspecified).AddTicks(8013), new TimeSpan(0, 3, 0, 0, 0)), Status.Closed }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "CreatorId", "Description", "Key", "Name", "ProjectUnitId", "UnitParentId", "UnitType" },
                values: new object[,]
                {
                    { 40, 2, null, new Guid("2da24682-8c31-4a23-b1e4-f979e8f80805"), "add doc document with api desc", null, 4, UnitType.Comment },
                    { 25, 2, null, new Guid("32ae9833-13f7-4350-a68e-70e0bfeeca30"), "Create postgres image", null, 5, UnitType.SubTask },
                    { 26, 2, null, new Guid("02d0d799-c713-4d50-997a-c4b116192153"), "Create dotnet image", null, 5, UnitType.SubTask },
                    { 41, 2, null, new Guid("d719805a-5c72-4473-8e6a-16b23120e185"), "we use postgres 11", null, 5, UnitType.Comment },
                    { 42, 1, null, new Guid("2e5bc155-4842-4bf3-94de-36199204d917"), "Ok", null, 5, UnitType.Comment }
                });

            migrationBuilder.InsertData(
                table: "TagOnTasks",
                columns: new[] { "TagId", "TaskId" },
                values: new object[,]
                {
                    { 5, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 1, 2 },
                    { 8, 2 },
                    { 6, 3 },
                    { 8, 3 }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssignedId", "MileStoneId", "UnitId" },
                values: new object[,]
                {
                    { 4, 2, 1, 4 },
                    { 5, 2, 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "TermInfos",
                columns: new[] { "UnitId", "DueTs", "StartTs", "Status" },
                values: new object[,]
                {
                    { 40, null, new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 548, DateTimeKind.Unspecified).AddTicks(8053), new TimeSpan(0, 3, 0, 0, 0)), Status.None },
                    { 25, null, new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 548, DateTimeKind.Unspecified).AddTicks(8036), new TimeSpan(0, 3, 0, 0, 0)), Status.InProgress },
                    { 26, null, new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 548, DateTimeKind.Unspecified).AddTicks(8046), new TimeSpan(0, 3, 0, 0, 0)), Status.Closed },
                    { 41, null, new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 548, DateTimeKind.Unspecified).AddTicks(8061), new TimeSpan(0, 3, 0, 0, 0)), Status.None },
                    { 42, null, new DateTimeOffset(new DateTime(2020, 6, 10, 11, 16, 4, 548, DateTimeKind.Unspecified).AddTicks(8069), new TimeSpan(0, 3, 0, 0, 0)), Status.None }
                });

            migrationBuilder.InsertData(
                table: "TagOnTasks",
                columns: new[] { "TagId", "TaskId" },
                values: new object[,]
                {
                    { 4, 4 },
                    { 8, 4 },
                    { 1, 5 },
                    { 10, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MileStones_UnitId",
                table: "MileStones",
                column: "UnitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TagOnTasks_TaskId",
                table: "TagOnTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedId",
                table: "Tasks",
                column: "AssignedId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_MileStoneId",
                table: "Tasks",
                column: "MileStoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UnitId",
                table: "Tasks",
                column: "UnitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_CreatorId",
                table: "Units",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_ProjectUnitId",
                table: "Units",
                column: "ProjectUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitParentId",
                table: "Units",
                column: "UnitParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Units_UnitId",
                table: "Tasks",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_MileStones_MileStoneId",
                table: "Tasks",
                column: "MileStoneId",
                principalTable: "MileStones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Projects_ProjectUnitId",
                table: "Units",
                column: "ProjectUnitId",
                principalTable: "Projects",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_AspNetUsers_CreatorId",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Units_UnitId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "TagOnTasks");

            migrationBuilder.DropTable(
                name: "TermInfos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "MileStones");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
