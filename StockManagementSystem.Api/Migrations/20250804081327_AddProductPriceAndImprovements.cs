using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagementSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProductPriceAndImprovements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GINItems_Products_ProductId",
                table: "GINItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNItems_Products_ProductId",
                table: "GRNItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GRNs_Date",
                table: "GRNs",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_GINs_Date",
                table: "GINs",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Timestamp",
                table: "AuditLogs",
                column: "Timestamp");

            migrationBuilder.AddForeignKey(
                name: "FK_GINItems_Products_ProductId",
                table: "GINItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNItems_Products_ProductId",
                table: "GRNItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GINItems_Products_ProductId",
                table: "GINItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNItems_Products_ProductId",
                table: "GRNItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SKU",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_GRNs_Date",
                table: "GRNs");

            migrationBuilder.DropIndex(
                name: "IX_GINs_Date",
                table: "GINs");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_Timestamp",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_GINItems_Products_ProductId",
                table: "GINItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNItems_Products_ProductId",
                table: "GRNItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
