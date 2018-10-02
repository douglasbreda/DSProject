using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSProject.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Integrant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    RG = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    HourOfBirth = table.Column<string>(nullable: true),
                    BirthPlace = table.Column<string>(nullable: true),
                    Sign = table.Column<string>(nullable: true),
                    IsVoluntary = table.Column<bool>(nullable: false),
                    Adress = table.Column<string>(nullable: true),
                    AdressNumber = table.Column<string>(nullable: true),
                    AdressDistrict = table.Column<string>(nullable: true),
                    AdressCity = table.Column<string>(nullable: true),
                    AdressState = table.Column<string>(nullable: true),
                    AdressCep = table.Column<string>(nullable: true),
                    NumberOfParticipations = table.Column<int>(nullable: false),
                    IsUseCar = table.Column<bool>(nullable: false),
                    Scholarity = table.Column<string>(nullable: true),
                    Institution = table.Column<string>(nullable: true),
                    Course = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    KnowStreets = table.Column<bool>(nullable: false),
                    AdvancedKnowledge = table.Column<string>(nullable: true),
                    AboutCollectors = table.Column<string>(nullable: true),
                    SomethingMore = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ability",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntegrantId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ability_Integrant_IntegrantId",
                        column: x => x.IntegrantId,
                        principalTable: "Integrant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntegrantId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_Integrant_IntegrantId",
                        column: x => x.IntegrantId,
                        principalTable: "Integrant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Knowledge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntegrantId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knowledge_Integrant_IntegrantId",
                        column: x => x.IntegrantId,
                        principalTable: "Integrant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntegrantId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Language_Integrant_IntegrantId",
                        column: x => x.IntegrantId,
                        principalTable: "Integrant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntegrantId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sports_Integrant_IntegrantId",
                        column: x => x.IntegrantId,
                        principalTable: "Integrant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ability_IntegrantId",
                table: "Ability",
                column: "IntegrantId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_IntegrantId",
                table: "Device",
                column: "IntegrantId");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_IntegrantId",
                table: "Knowledge",
                column: "IntegrantId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_IntegrantId",
                table: "Language",
                column: "IntegrantId");

            migrationBuilder.CreateIndex(
                name: "IX_Sports_IntegrantId",
                table: "Sports",
                column: "IntegrantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ability");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Knowledge");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Sports");

            migrationBuilder.DropTable(
                name: "Integrant");
        }
    }
}
