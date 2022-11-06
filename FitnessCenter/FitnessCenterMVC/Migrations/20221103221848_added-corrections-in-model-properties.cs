using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessCenterMVC.Migrations
{
    public partial class addedcorrectionsinmodelproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Term");

            migrationBuilder.RenameColumn(
                name: "NumberOfMembers",
                table: "Term",
                newName: "FreeSpace");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Workout",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Workout",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Workout");

            migrationBuilder.RenameColumn(
                name: "FreeSpace",
                table: "Term",
                newName: "NumberOfMembers");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Term",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
