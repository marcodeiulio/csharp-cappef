using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_cappef.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    student_email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "course_image",
                columns: table => new
                {
                    CourseImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_image", x => x.CourseImageId);
                    table.ForeignKey(
                        name: "FK_course_image_course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    FrequentedCoursesCourseId = table.Column<int>(type: "int", nullable: false),
                    StudentsEnrolledStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.FrequentedCoursesCourseId, x.StudentsEnrolledStudentId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_course_FrequentedCoursesCourseId",
                        column: x => x.FrequentedCoursesCourseId,
                        principalTable: "course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_student_StudentsEnrolledStudentId",
                        column: x => x.StudentsEnrolledStudentId,
                        principalTable: "student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_review_student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_course_image_CourseId",
                table: "course_image",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsEnrolledStudentId",
                table: "CourseStudent",
                column: "StudentsEnrolledStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_review_StudentId",
                table: "review",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_student_student_email",
                table: "student",
                column: "student_email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_image");

            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "student");
        }
    }
}
