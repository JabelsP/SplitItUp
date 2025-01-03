using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitItUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleToSpending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Spendings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Spendings");
        }
    }
}
