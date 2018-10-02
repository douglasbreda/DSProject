using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSProject.Migrations
{
    public partial class NewColumnsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUseCar",
                table: "Integrant",
                newName: "IsUseCarTests");

            migrationBuilder.AddColumn<string>(
                name: "CarBrand",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarColor",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarOwner",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarPlate",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarRenavam",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarYear",
                table: "Integrant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CellPhone",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverAuthorizedOne",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverAuthorizedTwo",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsCollectorOfSomething",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUseCarPlotting",
                table: "Integrant",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Integrant",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Integrant",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Instrument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntegrantId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instrument_Integrant_IntegrantId",
                        column: x => x.IntegrantId,
                        principalTable: "Integrant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentPlayed",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntegrantId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentPlayed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentPlayed_Integrant_IntegrantId",
                        column: x => x.IntegrantId,
                        principalTable: "Integrant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_IntegrantId",
                table: "Instrument",
                column: "IntegrantId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentPlayed_IntegrantId",
                table: "InstrumentPlayed",
                column: "IntegrantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instrument");

            migrationBuilder.DropTable(
                name: "InstrumentPlayed");

            migrationBuilder.DropColumn(
                name: "CarBrand",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "CarColor",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "CarOwner",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "CarPlate",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "CarRenavam",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "CarYear",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "CellPhone",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "DriverAuthorizedOne",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "DriverAuthorizedTwo",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "IsCollectorOfSomething",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "IsUseCarPlotting",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Integrant");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Integrant");

            migrationBuilder.RenameColumn(
                name: "IsUseCarTests",
                table: "Integrant",
                newName: "IsUseCar");
        }
    }
}
