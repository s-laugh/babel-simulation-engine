using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace esdc_simulation_api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaternityBenefitsSimulationCase",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MaxWeeklyAmount = table.Column<decimal>(nullable: false),
                    Percentage = table.Column<double>(nullable: false),
                    NumWeeks = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaternityBenefitsSimulationCase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Flsah = table.Column<string>(nullable: true),
                    AverageIncome = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimulationResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SimulationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simulations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BaseCaseId = table.Column<Guid>(nullable: false),
                    VariantCaseId = table.Column<Guid>(nullable: false),
                    SimulationName = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Simulations_MaternityBenefitsSimulationCase_BaseCaseId",
                        column: x => x.BaseCaseId,
                        principalTable: "MaternityBenefitsSimulationCase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Simulations_MaternityBenefitsSimulationCase_VariantCaseId",
                        column: x => x.VariantCaseId,
                        principalTable: "MaternityBenefitsSimulationCase",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MaternityBenefitsSimulationResultId = table.Column<Guid>(nullable: false),
                    MaternityBenefitsPersonId = table.Column<Guid>(nullable: false),
                    BaseAmount = table.Column<decimal>(nullable: false),
                    VariantAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonResult_Persons_MaternityBenefitsPersonId",
                        column: x => x.MaternityBenefitsPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonResult_SimulationResults_MaternityBenefitsSimulationResultId",
                        column: x => x.MaternityBenefitsSimulationResultId,
                        principalTable: "SimulationResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonResult_MaternityBenefitsPersonId",
                table: "PersonResult",
                column: "MaternityBenefitsPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonResult_MaternityBenefitsSimulationResultId",
                table: "PersonResult",
                column: "MaternityBenefitsSimulationResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_BaseCaseId",
                table: "Simulations",
                column: "BaseCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_VariantCaseId",
                table: "Simulations",
                column: "VariantCaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonResult");

            migrationBuilder.DropTable(
                name: "Simulations");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "SimulationResults");

            migrationBuilder.DropTable(
                name: "MaternityBenefitsSimulationCase");
        }
    }
}
