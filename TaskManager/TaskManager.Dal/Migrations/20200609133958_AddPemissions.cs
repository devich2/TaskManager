using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskManager.Entities.Enum;

namespace TaskManager.Dal.Migrations
{
    public partial class AddPemissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:PermissionType", "ProjectModify,ProjectDelete,MilestoneAdd,MilestoneModify,MilestoneDelete,SubTaskAdd,SubTaskModify,SubTaskDelete,TaskAdd,TaskModify,TaskDelete,CommentAdd,CommentModify,CommentDelete,RoleChange,StatusChange,UserInvite,UserKick,TagAdd,TagDelete,Read")
                .Annotation("Npgsql:Enum:Status", "None,Open,InProgress,Closed")
                .Annotation("Npgsql:Enum:UnitType", "Comment,Milestone,Project,Task,SubTask")
                .OldAnnotation("Npgsql:Enum:Status", "None,Open,InProgress,Closed")
                .OldAnnotation("Npgsql:Enum:UnitType", "Comment,Milestone,Project,Task,SubTask");

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

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "PermissionType", "RoleId" },
                values: new object[,]
                {
                    { 1, PermissionType.Read, 1 },
                    { 26, PermissionType.CommentAdd, 4 },
                    { 27, PermissionType.CommentDelete, 4 },
                    { 28, PermissionType.CommentModify, 4 },
                    { 29, PermissionType.MilestoneAdd, 4 },
                    { 30, PermissionType.MilestoneDelete, 4 },
                    { 31, PermissionType.MilestoneModify, 4 },
                    { 32, PermissionType.TaskAdd, 4 },
                    { 33, PermissionType.TaskDelete, 4 },
                    { 35, PermissionType.SubTaskAdd, 4 },
                    { 36, PermissionType.SubTaskDelete, 4 },
                    { 37, PermissionType.SubTaskModify, 4 },
                    { 38, PermissionType.ProjectDelete, 4 },
                    { 39, PermissionType.ProjectModify, 4 },
                    { 40, PermissionType.RoleChange, 4 },
                    { 41, PermissionType.UserInvite, 4 },
                    { 42, PermissionType.UserKick, 4 },
                    { 43, PermissionType.TagAdd, 4 },
                    { 44, PermissionType.TagDelete, 4 },
                    { 45, PermissionType.StatusChange, 4 },
                    { 25, PermissionType.Read, 4 },
                    { 24, PermissionType.StatusChange, 3 },
                    { 34, PermissionType.TaskModify, 4 },
                    { 22, PermissionType.TagAdd, 3 },
                    { 23, PermissionType.TagDelete, 3 },
                    { 3, PermissionType.CommentAdd, 2 },
                    { 4, PermissionType.CommentDelete, 2 },
                    { 5, PermissionType.CommentModify, 2 },
                    { 6, PermissionType.TagAdd, 2 },
                    { 7, PermissionType.TagDelete, 2 },
                    { 8, PermissionType.StatusChange, 2 },
                    { 9, PermissionType.Read, 3 },
                    { 10, PermissionType.CommentAdd, 3 },
                    { 11, PermissionType.CommentDelete, 3 },
                    { 2, PermissionType.Read, 2 },
                    { 13, PermissionType.MilestoneAdd, 3 },
                    { 14, PermissionType.MilestoneDelete, 3 },
                    { 15, PermissionType.MilestoneModify, 3 },
                    { 16, PermissionType.TaskAdd, 3 },
                    { 17, PermissionType.TaskDelete, 3 },
                    { 18, PermissionType.TaskModify, 3 },
                    { 19, PermissionType.SubTaskAdd, 3 },
                    { 20, PermissionType.SubTaskDelete, 3 },
                    { 21, PermissionType.SubTaskModify, 3 },
                    { 12, PermissionType.CommentModify, 3 }
                });

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 45, DateTimeKind.Unspecified).AddTicks(2221), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 49, DateTimeKind.Unspecified).AddTicks(7080), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 49, DateTimeKind.Unspecified).AddTicks(7192), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 49, DateTimeKind.Unspecified).AddTicks(7204), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 5,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 49, DateTimeKind.Unspecified).AddTicks(7212), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 20,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 49, DateTimeKind.Unspecified).AddTicks(7225), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 25,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 49, DateTimeKind.Unspecified).AddTicks(7233), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 26,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 49, DateTimeKind.Unspecified).AddTicks(7239), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 40,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 49, DateTimeKind.Unspecified).AddTicks(7246), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 41,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 49, DateTimeKind.Unspecified).AddTicks(7254), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 42,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 16, 39, 58, 49, DateTimeKind.Unspecified).AddTicks(7260), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "Key",
                value: new Guid("dea2c6f6-3064-40fb-9f75-8e695939e839"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "Key",
                value: new Guid("814d9772-ef7c-4eb9-a932-18dc89d4a0b4"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "Key",
                value: new Guid("a7d245d0-3280-4ef5-9acb-6787bc194db7"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "Key",
                value: new Guid("90992949-51c7-4ad1-aa92-086a1c57ba5d"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 5,
                column: "Key",
                value: new Guid("3310e655-5b08-493c-972c-13f668b5c57e"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 20,
                column: "Key",
                value: new Guid("bff26a36-6cb5-4cef-a7c4-939f6eaf76ca"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 25,
                column: "Key",
                value: new Guid("32ae9833-13f7-4350-a68e-70e0bfeeca30"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 26,
                column: "Key",
                value: new Guid("02d0d799-c713-4d50-997a-c4b116192153"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 40,
                column: "Key",
                value: new Guid("2da24682-8c31-4a23-b1e4-f979e8f80805"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 41,
                column: "Key",
                value: new Guid("d719805a-5c72-4473-8e6a-16b23120e185"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 42,
                column: "Key",
                value: new Guid("2e5bc155-4842-4bf3-94de-36199204d917"));

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:Status", "None,Open,InProgress,Closed")
                .Annotation("Npgsql:Enum:UnitType", "Comment,Milestone,Project,Task,SubTask")
                .OldAnnotation("Npgsql:Enum:PermissionType", "ProjectModify,ProjectDelete,MilestoneAdd,MilestoneModify,MilestoneDelete,SubTaskAdd,SubTaskModify,SubTaskDelete,TaskAdd,TaskModify,TaskDelete,CommentAdd,CommentModify,CommentDelete,RoleChange,StatusChange,UserInvite,UserKick,TagAdd,TagDelete,Read")
                .OldAnnotation("Npgsql:Enum:Status", "None,Open,InProgress,Closed")
                .OldAnnotation("Npgsql:Enum:UnitType", "Comment,Milestone,Project,Task,SubTask");

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 31, DateTimeKind.Unspecified).AddTicks(4532), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 34, DateTimeKind.Unspecified).AddTicks(1373), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 34, DateTimeKind.Unspecified).AddTicks(1480), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 34, DateTimeKind.Unspecified).AddTicks(1493), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 5,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 34, DateTimeKind.Unspecified).AddTicks(1629), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 20,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 34, DateTimeKind.Unspecified).AddTicks(1641), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 25,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 34, DateTimeKind.Unspecified).AddTicks(1648), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 26,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 34, DateTimeKind.Unspecified).AddTicks(1655), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 40,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 34, DateTimeKind.Unspecified).AddTicks(1661), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 41,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 34, DateTimeKind.Unspecified).AddTicks(1761), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 42,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 9, 14, 47, 59, 34, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "Key",
                value: new Guid("dea2c6f6-3064-40fb-9f75-8e695939e839"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "Key",
                value: new Guid("814d9772-ef7c-4eb9-a932-18dc89d4a0b4"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "Key",
                value: new Guid("a7d245d0-3280-4ef5-9acb-6787bc194db7"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "Key",
                value: new Guid("90992949-51c7-4ad1-aa92-086a1c57ba5d"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 5,
                column: "Key",
                value: new Guid("3310e655-5b08-493c-972c-13f668b5c57e"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 20,
                column: "Key",
                value: new Guid("bff26a36-6cb5-4cef-a7c4-939f6eaf76ca"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 25,
                column: "Key",
                value: new Guid("32ae9833-13f7-4350-a68e-70e0bfeeca30"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 26,
                column: "Key",
                value: new Guid("02d0d799-c713-4d50-997a-c4b116192153"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 40,
                column: "Key",
                value: new Guid("2da24682-8c31-4a23-b1e4-f979e8f80805"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 41,
                column: "Key",
                value: new Guid("d719805a-5c72-4473-8e6a-16b23120e185"));

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 42,
                column: "Key",
                value: new Guid("2e5bc155-4842-4bf3-94de-36199204d917"));
        }
    }
}
