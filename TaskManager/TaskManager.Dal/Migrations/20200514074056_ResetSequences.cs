using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Dal.Migrations
{
    public partial class ResetSequences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RestartSequence(name: "AspNetRoleClaims_Id_seq", 1000);
            migrationBuilder.RestartSequence(name: "AspNetRoles_Id_seq", 1000);
            migrationBuilder.RestartSequence(name: "AspNetUserClaims_Id_seq", 1000);
            migrationBuilder.RestartSequence(name: "AspNetUsers_Id_seq", 1000);
            migrationBuilder.RestartSequence(name: "ProjectMembers_Id_seq", 1000);
            migrationBuilder.RestartSequence(name: "Projects_Id_seq", 1000);
            migrationBuilder.RestartSequence(name: "Tags_Id_seq", 1000);
            migrationBuilder.RestartSequence(name: "Tasks_Id_seq", 1000);
            migrationBuilder.RestartSequence(name: "TermInfos_Id_seq", 1000);
            migrationBuilder.RestartSequence(name: "Units_Id_seq", 1000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
