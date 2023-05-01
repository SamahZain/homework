using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homework.Migrations
{
    public partial class alterstudentaddyear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Year",
                table: "Students",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Students");
        }
    }
}
