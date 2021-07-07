using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace esdc_simulation_api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaternityBenefitsPerson",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    SpokenLanguage = table.Column<string>(nullable: true),
                    EducationLevel = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    AverageIncome = table.Column<decimal>(type: "decimal(7, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaternityBenefitsPerson", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaternityBenefitsSimulationCase",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MaxWeeklyAmount = table.Column<decimal>(type: "decimal(7, 2)", nullable: false),
                    Percentage = table.Column<double>(nullable: false),
                    NumWeeks = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaternityBenefitsSimulationCase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaternityBenefitsSimulation",
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
                    table.PrimaryKey("PK_MaternityBenefitsSimulation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaternityBenefitsSimulation_MaternityBenefitsSimulationCase_BaseCaseId",
                        column: x => x.BaseCaseId,
                        principalTable: "MaternityBenefitsSimulationCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaternityBenefitsSimulation_MaternityBenefitsSimulationCase_VariantCaseId",
                        column: x => x.VariantCaseId,
                        principalTable: "MaternityBenefitsSimulationCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaternityBenefitsSimulationResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SimulationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaternityBenefitsSimulationResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaternityBenefitsSimulationResult_MaternityBenefitsSimulation_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "MaternityBenefitsSimulation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaternityBenefitsPersonResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SimulationResultId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    BaseAmount = table.Column<decimal>(type: "decimal(7, 2)", nullable: false),
                    VariantAmount = table.Column<decimal>(type: "decimal(7, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaternityBenefitsPersonResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaternityBenefitsPersonResult_MaternityBenefitsPerson_PersonId",
                        column: x => x.PersonId,
                        principalTable: "MaternityBenefitsPerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaternityBenefitsPersonResult_MaternityBenefitsSimulationResult_SimulationResultId",
                        column: x => x.SimulationResultId,
                        principalTable: "MaternityBenefitsSimulationResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaternityBenefitsPersonResult_PersonId",
                table: "MaternityBenefitsPersonResult",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_MaternityBenefitsPersonResult_SimulationResultId",
                table: "MaternityBenefitsPersonResult",
                column: "SimulationResultId");

            migrationBuilder.CreateIndex(
                name: "IX_MaternityBenefitsSimulation_BaseCaseId",
                table: "MaternityBenefitsSimulation",
                column: "BaseCaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaternityBenefitsSimulation_VariantCaseId",
                table: "MaternityBenefitsSimulation",
                column: "VariantCaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaternityBenefitsSimulationResult_SimulationId",
                table: "MaternityBenefitsSimulationResult",
                column: "SimulationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaternityBenefitsPersonResult");

            migrationBuilder.DropTable(
                name: "MaternityBenefitsPerson");

            migrationBuilder.DropTable(
                name: "MaternityBenefitsSimulationResult");

            migrationBuilder.DropTable(
                name: "MaternityBenefitsSimulation");

            migrationBuilder.DropTable(
                name: "MaternityBenefitsSimulationCase");
        }
    }
}
