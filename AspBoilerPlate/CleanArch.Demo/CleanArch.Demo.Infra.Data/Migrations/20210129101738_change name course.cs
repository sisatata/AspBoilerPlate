using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArch.Demo.Infra.Data.Migrations
{
    public partial class changenamecourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesNew",
                table: "CoursesNew");

            migrationBuilder.RenameTable(
                name: "CoursesNew",
                newName: "Courses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "CoursesNew");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesNew",
                table: "CoursesNew",
                column: "Id");
        }
    }
}
