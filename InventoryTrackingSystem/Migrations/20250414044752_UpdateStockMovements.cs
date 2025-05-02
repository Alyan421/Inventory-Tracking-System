using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStockMovements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "StockMovements",
                newName: "CreatedByName");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "StockMovements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "StockMovements");

            migrationBuilder.RenameColumn(
                name: "CreatedByName",
                table: "StockMovements",
                newName: "CreatedBy");
        }
    }
}
