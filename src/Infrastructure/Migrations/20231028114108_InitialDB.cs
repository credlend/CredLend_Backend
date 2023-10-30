using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReturnRate = table.Column<float>(type: "REAL", nullable: false),
                    ReturnDeadLine = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TypePlan = table.Column<string>(type: "TEXT", nullable: false),
                    ValuePlan = table.Column<float>(type: "REAL", nullable: false),
                    TransactionWay = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PaymentTerm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InterestRate = table.Column<float>(type: "REAL", nullable: false),
                    TypePlan = table.Column<string>(type: "TEXT", nullable: false),
                    ValuePlan = table.Column<float>(type: "REAL", nullable: false),
                    TransactionWay = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsAdm = table.Column<bool>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentPlan");

            migrationBuilder.DropTable(
                name: "LoanPlan");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
