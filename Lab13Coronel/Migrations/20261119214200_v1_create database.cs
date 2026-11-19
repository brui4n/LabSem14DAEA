using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab13Coronel.Migrations
{
    /// <inheritdoc />
    public partial class v1_createdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    idCourse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.idCourse);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    idGrade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.idGrade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    idStudent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade_idGrade = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.idStudent);
                    table.ForeignKey(
                        name: "FK_Students_Grades_Grade_idGrade",
                        column: x => x.Grade_idGrade,
                        principalTable: "Grades",
                        principalColumn: "idGrade",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    idEnrollment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_idStudent = table.Column<int>(type: "int", nullable: false),
                    Course_idCourse = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.idEnrollment);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_Course_idCourse",
                        column: x => x.Course_idCourse,
                        principalTable: "Courses",
                        principalColumn: "idCourse",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_Student_idStudent",
                        column: x => x.Student_idStudent,
                        principalTable: "Students",
                        principalColumn: "idStudent",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_Course_idCourse",
                table: "Enrollments",
                column: "Course_idCourse");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_Student_idStudent",
                table: "Enrollments",
                column: "Student_idStudent");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Grade_idGrade",
                table: "Students",
                column: "Grade_idGrade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}
