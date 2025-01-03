using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitItUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSpendingShare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpendingShares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    PercentageOfSpending = table.Column<double>(type: "double precision", nullable: false),
                    Settled = table.Column<bool>(type: "boolean", nullable: false),
                    SettledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SpendingId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpendingShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpendingShares_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpendingShares_Spendings_SpendingId",
                        column: x => x.SpendingId,
                        principalTable: "Spendings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpendingShares_PersonId",
                table: "SpendingShares",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SpendingShares_SpendingId",
                table: "SpendingShares",
                column: "SpendingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpendingShares");
        }
    }
}
