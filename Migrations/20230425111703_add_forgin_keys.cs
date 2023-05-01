using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homework.Migrations
{
    public partial class add_forgin_keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "Term",
                table: "Subjects",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "SubjectLectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_DeptId",
                table: "Subjects",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectLectures_SubjectId",
                table: "SubjectLectures",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectLectures_Subjects_SubjectId",
                table: "SubjectLectures",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Departments_DeptId",
                table: "Subjects",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectLectures_Subjects_SubjectId",
                table: "SubjectLectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Departments_DeptId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_DeptId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_SubjectLectures_SubjectId",
                table: "SubjectLectures");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Term",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "SubjectLectures");
        }
    }
}
