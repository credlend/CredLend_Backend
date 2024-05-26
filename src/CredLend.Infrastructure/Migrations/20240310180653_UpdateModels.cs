using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OperationsLoanPlan_UserID",
                table: "OperationsLoanPlan",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsInvestmentPlan_UserID",
                table: "OperationsInvestmentPlan",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsInvestmentPlan_AspNetUsers_UserID",
                table: "OperationsInvestmentPlan",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsLoanPlan_AspNetUsers_UserID",
                table: "OperationsLoanPlan",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsInvestmentPlan_AspNetUsers_UserID",
                table: "OperationsInvestmentPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsLoanPlan_AspNetUsers_UserID",
                table: "OperationsLoanPlan");

            migrationBuilder.DropIndex(
                name: "IX_OperationsLoanPlan_UserID",
                table: "OperationsLoanPlan");

            migrationBuilder.DropIndex(
                name: "IX_OperationsInvestmentPlan_UserID",
                table: "OperationsInvestmentPlan");
        }
    }
}
