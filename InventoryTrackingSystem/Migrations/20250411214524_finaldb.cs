using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class finaldb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockID",
                table: "StockMovements",
                newName: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductStocks_ProductId",
                table: "StoreProductStocks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductStocks_StoreId",
                table: "StoreProductStocks",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ProductId",
                table: "StockMovements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_StoreId",
                table: "StockMovements",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_Products_ProductId",
                table: "StockMovements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_Stores_StoreId",
                table: "StockMovements",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProductStocks_Products_ProductId",
                table: "StoreProductStocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProductStocks_Stores_StoreId",
                table: "StoreProductStocks",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_Products_ProductId",
                table: "StockMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_Stores_StoreId",
                table: "StockMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProductStocks_Products_ProductId",
                table: "StoreProductStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProductStocks_Stores_StoreId",
                table: "StoreProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_StoreProductStocks_ProductId",
                table: "StoreProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_StoreProductStocks_StoreId",
                table: "StoreProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_ProductId",
                table: "StockMovements");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_StoreId",
                table: "StockMovements");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "StockMovements",
                newName: "StockID");
        }
    }
}
