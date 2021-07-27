using Microsoft.EntityFrameworkCore.Migrations;

namespace esdc_simulation_api.Migrations
{
    public partial class PersonResultCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaternityBenefitsPersonResult_MaternityBenefitsPerson_PersonId",
                table: "MaternityBenefitsPersonResult");

            migrationBuilder.AddForeignKey(
                name: "FK_MaternityBenefitsPersonResult_MaternityBenefitsPerson_PersonId",
                table: "MaternityBenefitsPersonResult",
                column: "PersonId",
                principalTable: "MaternityBenefitsPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaternityBenefitsPersonResult_MaternityBenefitsPerson_PersonId",
                table: "MaternityBenefitsPersonResult");

            migrationBuilder.AddForeignKey(
                name: "FK_MaternityBenefitsPersonResult_MaternityBenefitsPerson_PersonId",
                table: "MaternityBenefitsPersonResult",
                column: "PersonId",
                principalTable: "MaternityBenefitsPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
