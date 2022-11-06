using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessCenterMVC.Migrations
{
    public partial class deletedyears_of_experience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
