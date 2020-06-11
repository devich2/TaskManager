using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Dal.Migrations
{
    public partial class RestartSequences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RestartSequence("AspNetRoleClaims_Id_seq", 1000);
            migrationBuilder.RestartSequence("AspNetRoles_Id_seq", 1000);
            migrationBuilder.RestartSequence("AspNetUserClaims_Id_seq", 1000);
            migrationBuilder.RestartSequence("AspNetUsers_Id_seq", 1000);
            migrationBuilder.RestartSequence("AspNetRoleClaims_Id_seq", 1000);
            migrationBuilder.RestartSequence("MileStones_Id_seq", 1000);
            migrationBuilder.RestartSequence("Permissions_Id_seq", 1000);
            migrationBuilder.RestartSequence("Tags_Id_seq", 1000);
            migrationBuilder.RestartSequence("Tasks_Id_seq", 1000);
            migrationBuilder.RestartSequence("Units_UnitId_seq", 1000);
        }
    }
}
