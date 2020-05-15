using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TaskManager.Dal.Migrations
{
    public partial class UnitId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Units_UnitId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RelationShips_Units_ParentUnitId",
                table: "RelationShips");

            migrationBuilder.DropForeignKey(
                name: "FK_RelationShips_Units_UnitId",
                table: "RelationShips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Units_UnitId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TermInfos_Units_UnitId",
                table: "TermInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Units");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Units",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "UnitId");

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "CreatorId", "Description", "Key", "Name", "UnitType" },
                values: new object[,]
                {
                    { 1, 2, "Create user api, spam list and blocking users", new Guid("dea2c6f6-3064-40fb-9f75-8e695939e839"), "CRUD API creating and deleting users", 3 },
                    { 2, 2, "Modify database, add email service for client sales", new Guid("814d9772-ef7c-4eb9-a932-18dc89d4a0b4"), "Api for email subscriptions", 3 },
                    { 3, 2, "Change behaviour from delete cascade to restrict and rework service deleting logic", new Guid("a7d245d0-3280-4ef5-9acb-6787bc194db7"), "Delete cascade", 3 },
                    { 4, 2, "Plug in PayPal", new Guid("90992949-51c7-4ad1-aa92-086a1c57ba5d"), "Api for donations", 3 },
                    { 5, 2, "Config docker compose with dotnet and postgres image and write integration tests for content with", new Guid("3310e655-5b08-493c-972c-13f668b5c57e"), "Testing content", 3 },
                    { 20, 3, "Система отслеживания заданий. Выдача задания менеджером. Статус задания, согласно рабочему процессу. Процент выполнения. Почтовые уведомления клиентам системы. Управление пользователями и их ролями.", new Guid("bff26a36-6cb5-4cef-a7c4-939f6eaf76ca"), "TaskManager", 2 },
                    { 25, 2, null, new Guid("32ae9833-13f7-4350-a68e-70e0bfeeca30"), "Create postgres image", 4 },
                    { 26, 2, null, new Guid("02d0d799-c713-4d50-997a-c4b116192153"), "Create dotnet image", 4 },
                    { 40, 2, null, new Guid("2da24682-8c31-4a23-b1e4-f979e8f80805"), "add doc document with api desc", 0 },
                    { 41, 2, null, new Guid("d719805a-5c72-4473-8e6a-16b23120e185"), "we use postgres 11", 0 },
                    { 42, 1, null, new Guid("2e5bc155-4842-4bf3-94de-36199204d917"), "Ok", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Units_UnitId",
                table: "Projects",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RelationShips_Units_ParentUnitId",
                table: "RelationShips",
                column: "ParentUnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RelationShips_Units_UnitId",
                table: "RelationShips",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Units_UnitId",
                table: "Tasks",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TermInfos_Units_UnitId",
                table: "TermInfos",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 856, DateTimeKind.Unspecified).AddTicks(5566), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 859, DateTimeKind.Unspecified).AddTicks(5065), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 859, DateTimeKind.Unspecified).AddTicks(5165), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 859, DateTimeKind.Unspecified).AddTicks(5177), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 859, DateTimeKind.Unspecified).AddTicks(5185), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 20,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 859, DateTimeKind.Unspecified).AddTicks(5197), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 25,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 859, DateTimeKind.Unspecified).AddTicks(5205), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 26,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 859, DateTimeKind.Unspecified).AddTicks(5212), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 40,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 859, DateTimeKind.Unspecified).AddTicks(5218), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 41,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 859, DateTimeKind.Unspecified).AddTicks(5226), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 42,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 17, 16, 56, 859, DateTimeKind.Unspecified).AddTicks(5233), new TimeSpan(0, 3, 0, 0, 0)));

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Units_UnitId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RelationShips_Units_ParentUnitId",
                table: "RelationShips");

            migrationBuilder.DropForeignKey(
                name: "FK_RelationShips_Units_UnitId",
                table: "RelationShips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Units_UnitId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TermInfos_Units_UnitId",
                table: "TermInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 42);

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Units");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Units",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "CreatorId", "Description", "Key", "Name", "UnitType" },
                values: new object[,]
                {
                    { 1, 2, "Create user api, spam list and blocking users", new Guid("dea2c6f6-3064-40fb-9f75-8e695939e839"), "CRUD API creating and deleting users", 3 },
                    { 2, 2, "Modify database, add email service for client sales", new Guid("814d9772-ef7c-4eb9-a932-18dc89d4a0b4"), "Api for email subscriptions", 3 },
                    { 3, 2, "Change behaviour from delete cascade to restrict and rework service deleting logic", new Guid("a7d245d0-3280-4ef5-9acb-6787bc194db7"), "Delete cascade", 3 },
                    { 4, 2, "Plug in PayPal", new Guid("90992949-51c7-4ad1-aa92-086a1c57ba5d"), "Api for donations", 3 },
                    { 5, 2, "Config docker compose with dotnet and postgres image and write integration tests for content with", new Guid("3310e655-5b08-493c-972c-13f668b5c57e"), "Testing content", 3 },
                    { 20, 3, "Система отслеживания заданий. Выдача задания менеджером. Статус задания, согласно рабочему процессу. Процент выполнения. Почтовые уведомления клиентам системы. Управление пользователями и их ролями.", new Guid("bff26a36-6cb5-4cef-a7c4-939f6eaf76ca"), "TaskManager", 2 },
                    { 25, 2, null, new Guid("32ae9833-13f7-4350-a68e-70e0bfeeca30"), "Create postgres image", 4 },
                    { 26, 2, null, new Guid("02d0d799-c713-4d50-997a-c4b116192153"), "Create dotnet image", 4 },
                    { 40, 2, null, new Guid("2da24682-8c31-4a23-b1e4-f979e8f80805"), "add doc document with api desc", 0 },
                    { 41, 2, null, new Guid("d719805a-5c72-4473-8e6a-16b23120e185"), "we use postgres 11", 0 },
                    { 42, 1, null, new Guid("2e5bc155-4842-4bf3-94de-36199204d917"), "Ok", 0 }
                });
            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Units_UnitId",
                table: "Projects",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RelationShips_Units_ParentUnitId",
                table: "RelationShips",
                column: "ParentUnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RelationShips_Units_UnitId",
                table: "RelationShips",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Units_UnitId",
                table: "Tasks",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TermInfos_Units_UnitId",
                table: "TermInfos",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 987, DateTimeKind.Unspecified).AddTicks(1063), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 990, DateTimeKind.Unspecified).AddTicks(761), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 990, DateTimeKind.Unspecified).AddTicks(863), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 990, DateTimeKind.Unspecified).AddTicks(875), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 990, DateTimeKind.Unspecified).AddTicks(882), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 20,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 990, DateTimeKind.Unspecified).AddTicks(896), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 25,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 990, DateTimeKind.Unspecified).AddTicks(903), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 26,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 990, DateTimeKind.Unspecified).AddTicks(910), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 40,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 990, DateTimeKind.Unspecified).AddTicks(917), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 41,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 990, DateTimeKind.Unspecified).AddTicks(925), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "Id",
                keyValue: 42,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 5, 15, 1, 34, 47, 990, DateTimeKind.Unspecified).AddTicks(932), new TimeSpan(0, 3, 0, 0, 0)));

           
        }
    }
}
