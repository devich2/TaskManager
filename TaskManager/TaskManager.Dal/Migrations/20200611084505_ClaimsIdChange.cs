using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Dal.Migrations
{
    public partial class ClaimsIdChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClaimValue",
                value: "Maintainer_20");

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClaimValue",
                value: "Owner_20");

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClaimValue",
                value: "Developer_20");

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 187, DateTimeKind.Unspecified).AddTicks(4439), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(765), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(848), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(860), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 5,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(868), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 20,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(880), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 25,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(887), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 26,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(893), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 40,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(900), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 41,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(907), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 42,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(913), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 50,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 45, 5, 190, DateTimeKind.Unspecified).AddTicks(920), new TimeSpan(0, 3, 0, 0, 0)));

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
            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClaimValue",
                value: "Maintainer_1");

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClaimValue",
                value: "Owner_1");

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClaimValue",
                value: "Developer_1");

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 214, DateTimeKind.Unspecified).AddTicks(6856), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4585), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4687), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4699), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 5,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4707), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 20,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4719), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 25,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4726), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 26,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4733), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 40,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4739), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 41,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4747), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 42,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4754), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 50,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 11, 42, 25, 218, DateTimeKind.Unspecified).AddTicks(4760), new TimeSpan(0, 3, 0, 0, 0)));

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
