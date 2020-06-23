using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Dal.Migrations
{
    public partial class SeedProjMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastLoginDate", "RegistrationDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 764, DateTimeKind.Unspecified).AddTicks(8754), new TimeSpan(0, 3, 0, 0, 0)), new DateTime(2020, 6, 13, 20, 44, 48, 764, DateTimeKind.Local).AddTicks(9573) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastLoginDate", "RegistrationDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 765, DateTimeKind.Unspecified).AddTicks(1596), new TimeSpan(0, 3, 0, 0, 0)), new DateTime(2020, 6, 13, 20, 44, 48, 765, DateTimeKind.Local).AddTicks(1654) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LastLoginDate", "RegistrationDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 765, DateTimeKind.Unspecified).AddTicks(1692), new TimeSpan(0, 3, 0, 0, 0)), new DateTime(2020, 6, 13, 20, 44, 48, 765, DateTimeKind.Local).AddTicks(1699) });

            migrationBuilder.InsertData(
                table: "ProjectMembers",
                columns: new[] { "UserId", "ProjectId", "GivenAccess" },
                values: new object[,]
                {
                    { 1, 20, new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 763, DateTimeKind.Unspecified).AddTicks(3587), new TimeSpan(0, 3, 0, 0, 0)) },
                    { 3, 20, new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 763, DateTimeKind.Unspecified).AddTicks(4106), new TimeSpan(0, 3, 0, 0, 0)) },
                    { 2, 20, new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 763, DateTimeKind.Unspecified).AddTicks(4072), new TimeSpan(0, 3, 0, 0, 0)) }
                });

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 758, DateTimeKind.Unspecified).AddTicks(399), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(638), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(728), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(741), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 5,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(749), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 20,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(761), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 25,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(768), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 26,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(775), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 40,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(781), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 41,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(789), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 42,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(795), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 50,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 20, 44, 48, 761, DateTimeKind.Unspecified).AddTicks(801), new TimeSpan(0, 3, 0, 0, 0)));

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

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 50,
                column: "Key",
                value: new Guid("2e5bc155-4842-4bf3-94de-36194204d917"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectMembers",
                keyColumns: new[] { "UserId", "ProjectId" },
                keyValues: new object[] { 1, 20 });

            migrationBuilder.DeleteData(
                table: "ProjectMembers",
                keyColumns: new[] { "UserId", "ProjectId" },
                keyValues: new object[] { 2, 20 });

            migrationBuilder.DeleteData(
                table: "ProjectMembers",
                keyColumns: new[] { "UserId", "ProjectId" },
                keyValues: new object[] { 3, 20 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastLoginDate", "RegistrationDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 319, DateTimeKind.Unspecified).AddTicks(7308), new TimeSpan(0, 3, 0, 0, 0)), new DateTime(2020, 6, 13, 19, 44, 21, 319, DateTimeKind.Local).AddTicks(8166) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastLoginDate", "RegistrationDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 320, DateTimeKind.Unspecified).AddTicks(1959), new TimeSpan(0, 3, 0, 0, 0)), new DateTime(2020, 6, 13, 19, 44, 21, 320, DateTimeKind.Local).AddTicks(2011) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LastLoginDate", "RegistrationDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 320, DateTimeKind.Unspecified).AddTicks(2039), new TimeSpan(0, 3, 0, 0, 0)), new DateTime(2020, 6, 13, 19, 44, 21, 320, DateTimeKind.Local).AddTicks(2046) });

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 312, DateTimeKind.Unspecified).AddTicks(1779), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5588), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5690), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5701), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 5,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5711), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 20,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5722), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 25,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5730), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 26,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5736), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 40,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5742), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 41,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5750), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 42,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5756), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 50,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 13, 19, 44, 21, 315, DateTimeKind.Unspecified).AddTicks(5762), new TimeSpan(0, 3, 0, 0, 0)));

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

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: 50,
                column: "Key",
                value: new Guid("2e5bc155-4842-4bf3-94de-36194204d917"));
        }
    }
}
