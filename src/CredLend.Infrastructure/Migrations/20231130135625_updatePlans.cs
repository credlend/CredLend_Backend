using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatePlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypePlan",
                table: "LoanPlan");

            migrationBuilder.DropColumn(
                name: "TypePlan",
                table: "InvestmentPlan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypePlan",
                table: "LoanPlan",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypePlan",
                table: "InvestmentPlan",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
