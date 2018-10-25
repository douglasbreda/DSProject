using Microsoft.EntityFrameworkCore.Migrations;

namespace DSProject.Migrations
{
    public partial class updateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChallengesYouParticipated",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyPhone",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FoodRestriction",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Preference",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShirtSize",
                table: "Integrant",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "ChallengesYouParticipated",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "EmergencyPhone",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "FoodRestriction",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "Preference",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "ShirtSize",
                table: "Integrant");
        }
    }
}
