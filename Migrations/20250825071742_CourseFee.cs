using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApStudentEnrolment.Migrations
{
    /// <inheritdoc />
    public partial class CourseFee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CourseFee",
                table: "Courses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseFee",
                table: "Courses");
        }
    }
}
