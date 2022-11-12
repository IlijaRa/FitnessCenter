using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessCenterMVC.Migrations
{
    public partial class addedpropertycoachrateinfitnessmemberworkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "FitnessMemberWorkout",
                newName: "WorkoutRate");

            migrationBuilder.AddColumn<double>(
                name: "CoachRate",
                table: "FitnessMemberWorkout",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoachRate",
                table: "FitnessMemberWorkout");

            migrationBuilder.RenameColumn(
                name: "WorkoutRate",
                table: "FitnessMemberWorkout",
                newName: "Rate");
        }
    }
}
