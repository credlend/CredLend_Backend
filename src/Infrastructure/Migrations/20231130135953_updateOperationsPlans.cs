using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateOperationsPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypePlan",
                table: "OperationsLoanPlan");

            migrationBuilder.DropColumn(
                name: "TypePlan",
                table: "OperationsInvestmentPlan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypePlan",
                table: "OperationsLoanPlan",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypePlan",
                table: "OperationsInvestmentPlan",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
