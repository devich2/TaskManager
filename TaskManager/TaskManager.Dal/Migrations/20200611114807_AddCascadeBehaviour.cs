using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Dal.Migrations
{
    public partial class AddCascadeBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Units_UnitParentId",
                table: "Units");

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 714, DateTimeKind.Unspecified).AddTicks(3361), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(488), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(578), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(589), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 5,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(596), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 20,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(608), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 25,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(617), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 26,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(624), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 40,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(630), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 41,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(638), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 42,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(644), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 50,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 14, 48, 6, 717, DateTimeKind.Unspecified).AddTicks(651), new TimeSpan(0, 3, 0, 0, 0)));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Units_UnitParentId",
                table: "Units",
                column: "UnitParentId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Units_UnitParentId",
                table: "Units");

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 1,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 337, DateTimeKind.Unspecified).AddTicks(1243), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 2,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(5966), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(6061), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 4,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(6074), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 5,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(6082), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 20,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(6094), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 25,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(6101), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 26,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(6108), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 40,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(6115), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 41,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(6123), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 42,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(6129), new TimeSpan(0, 3, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "TermInfos",
                keyColumn: "UnitId",
                keyValue: 50,
                column: "StartTs",
                value: new DateTimeOffset(new DateTime(2020, 6, 11, 13, 20, 8, 353, DateTimeKind.Unspecified).AddTicks(6136), new TimeSpan(0, 3, 0, 0, 0)));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Units_UnitParentId",
                table: "Units",
                column: "UnitParentId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
