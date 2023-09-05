using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApi.Migrations
{
    /// <inheritdoc />
    public partial class Uniqueness02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Courses_CollegeId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_Id_Name",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CollegeId_Name",
                table: "Courses",
                columns: new[] { "CollegeId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Courses_CollegeId_Name",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CollegeId",
                table: "Courses",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Id_Name",
                table: "Courses",
                columns: new[] { "Id", "Name" },
                unique: true);
        }
    }
}
