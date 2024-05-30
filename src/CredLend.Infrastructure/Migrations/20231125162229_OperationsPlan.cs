using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OperationsPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationsInvestmentPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReturnRate = table.Column<double>(type: "REAL", nullable: false),
                    ReturnDeadLine = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TypePlan = table.Column<string>(type: "TEXT", nullable: false),
                    ValuePlan = table.Column<double>(type: "REAL", nullable: false),
                    TransactionWay = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsInvestmentPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationsLoanPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PaymentTerm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InterestRate = table.Column<double>(type: "REAL", nullable: false),
                    TypePlan = table.Column<string>(type: "TEXT", nullable: false),
                    ValuePlan = table.Column<double>(type: "REAL", nullable: false),
                    TransactionWay = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsLoanPlan", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationsInvestmentPlan");

            migrationBuilder.DropTable(
                name: "OperationsLoanPlan");
        }
    }
}
