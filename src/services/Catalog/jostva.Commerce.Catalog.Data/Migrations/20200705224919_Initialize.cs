using Microsoft.EntityFrameworkCore.Migrations;

namespace jostva.Commerce.Catalog.Data.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "Catalog",
                columns: table => new
                {
                    ProductInStockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ProductInStockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Catalog",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description for Product 1", "Product 1", 992m },
                    { 73, "Description for Product 73", "Product 73", 971m },
                    { 72, "Description for Product 72", "Product 72", 108m },
                    { 71, "Description for Product 71", "Product 71", 965m },
                    { 70, "Description for Product 70", "Product 70", 366m },
                    { 69, "Description for Product 69", "Product 69", 817m },
                    { 68, "Description for Product 68", "Product 68", 658m },
                    { 67, "Description for Product 67", "Product 67", 554m },
                    { 66, "Description for Product 66", "Product 66", 279m },
                    { 65, "Description for Product 65", "Product 65", 342m },
                    { 64, "Description for Product 64", "Product 64", 143m },
                    { 63, "Description for Product 63", "Product 63", 262m },
                    { 62, "Description for Product 62", "Product 62", 702m },
                    { 61, "Description for Product 61", "Product 61", 847m },
                    { 60, "Description for Product 60", "Product 60", 519m },
                    { 59, "Description for Product 59", "Product 59", 321m },
                    { 58, "Description for Product 58", "Product 58", 561m },
                    { 57, "Description for Product 57", "Product 57", 110m },
                    { 56, "Description for Product 56", "Product 56", 948m },
                    { 55, "Description for Product 55", "Product 55", 325m },
                    { 54, "Description for Product 54", "Product 54", 627m },
                    { 53, "Description for Product 53", "Product 53", 560m },
                    { 74, "Description for Product 74", "Product 74", 928m },
                    { 52, "Description for Product 52", "Product 52", 421m },
                    { 75, "Description for Product 75", "Product 75", 581m },
                    { 77, "Description for Product 77", "Product 77", 541m },
                    { 98, "Description for Product 98", "Product 98", 558m },
                    { 97, "Description for Product 97", "Product 97", 880m },
                    { 96, "Description for Product 96", "Product 96", 908m },
                    { 95, "Description for Product 95", "Product 95", 127m },
                    { 94, "Description for Product 94", "Product 94", 953m },
                    { 93, "Description for Product 93", "Product 93", 310m },
                    { 92, "Description for Product 92", "Product 92", 395m },
                    { 91, "Description for Product 91", "Product 91", 905m },
                    { 90, "Description for Product 90", "Product 90", 334m },
                    { 89, "Description for Product 89", "Product 89", 190m },
                    { 88, "Description for Product 88", "Product 88", 915m },
                    { 87, "Description for Product 87", "Product 87", 188m },
                    { 86, "Description for Product 86", "Product 86", 651m },
                    { 85, "Description for Product 85", "Product 85", 282m },
                    { 84, "Description for Product 84", "Product 84", 178m },
                    { 83, "Description for Product 83", "Product 83", 319m },
                    { 82, "Description for Product 82", "Product 82", 456m },
                    { 81, "Description for Product 81", "Product 81", 561m },
                    { 80, "Description for Product 80", "Product 80", 966m },
                    { 79, "Description for Product 79", "Product 79", 686m },
                    { 78, "Description for Product 78", "Product 78", 829m },
                    { 76, "Description for Product 76", "Product 76", 368m },
                    { 51, "Description for Product 51", "Product 51", 708m },
                    { 50, "Description for Product 50", "Product 50", 763m },
                    { 49, "Description for Product 49", "Product 49", 177m },
                    { 22, "Description for Product 22", "Product 22", 143m },
                    { 21, "Description for Product 21", "Product 21", 411m },
                    { 20, "Description for Product 20", "Product 20", 193m },
                    { 19, "Description for Product 19", "Product 19", 803m },
                    { 18, "Description for Product 18", "Product 18", 444m },
                    { 17, "Description for Product 17", "Product 17", 540m },
                    { 16, "Description for Product 16", "Product 16", 822m },
                    { 15, "Description for Product 15", "Product 15", 281m },
                    { 14, "Description for Product 14", "Product 14", 101m },
                    { 13, "Description for Product 13", "Product 13", 974m },
                    { 12, "Description for Product 12", "Product 12", 996m },
                    { 11, "Description for Product 11", "Product 11", 828m },
                    { 10, "Description for Product 10", "Product 10", 314m },
                    { 9, "Description for Product 9", "Product 9", 934m },
                    { 8, "Description for Product 8", "Product 8", 103m },
                    { 7, "Description for Product 7", "Product 7", 999m },
                    { 6, "Description for Product 6", "Product 6", 413m },
                    { 5, "Description for Product 5", "Product 5", 664m },
                    { 4, "Description for Product 4", "Product 4", 859m },
                    { 3, "Description for Product 3", "Product 3", 272m },
                    { 2, "Description for Product 2", "Product 2", 615m },
                    { 23, "Description for Product 23", "Product 23", 605m },
                    { 24, "Description for Product 24", "Product 24", 710m },
                    { 25, "Description for Product 25", "Product 25", 638m },
                    { 26, "Description for Product 26", "Product 26", 244m },
                    { 48, "Description for Product 48", "Product 48", 209m },
                    { 47, "Description for Product 47", "Product 47", 683m },
                    { 46, "Description for Product 46", "Product 46", 671m },
                    { 45, "Description for Product 45", "Product 45", 124m },
                    { 44, "Description for Product 44", "Product 44", 775m },
                    { 43, "Description for Product 43", "Product 43", 601m },
                    { 42, "Description for Product 42", "Product 42", 142m },
                    { 41, "Description for Product 41", "Product 41", 611m },
                    { 40, "Description for Product 40", "Product 40", 874m },
                    { 39, "Description for Product 39", "Product 39", 473m },
                    { 99, "Description for Product 99", "Product 99", 533m },
                    { 38, "Description for Product 38", "Product 38", 532m },
                    { 36, "Description for Product 36", "Product 36", 915m },
                    { 35, "Description for Product 35", "Product 35", 283m },
                    { 34, "Description for Product 34", "Product 34", 416m },
                    { 33, "Description for Product 33", "Product 33", 542m },
                    { 32, "Description for Product 32", "Product 32", 648m },
                    { 31, "Description for Product 31", "Product 31", 972m },
                    { 30, "Description for Product 30", "Product 30", 890m },
                    { 29, "Description for Product 29", "Product 29", 841m },
                    { 28, "Description for Product 28", "Product 28", 397m },
                    { 27, "Description for Product 27", "Product 27", 565m },
                    { 37, "Description for Product 37", "Product 37", 857m },
                    { 100, "Description for Product 100", "Product 100", 844m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 79 },
                    { 73, 73, 97 },
                    { 72, 72, 7 },
                    { 71, 71, 3 },
                    { 70, 70, 6 },
                    { 69, 69, 93 },
                    { 68, 68, 18 },
                    { 67, 67, 31 },
                    { 66, 66, 88 },
                    { 65, 65, 77 },
                    { 64, 64, 15 },
                    { 63, 63, 64 },
                    { 62, 62, 19 },
                    { 61, 61, 29 },
                    { 60, 60, 35 },
                    { 59, 59, 34 },
                    { 58, 58, 9 },
                    { 57, 57, 35 },
                    { 56, 56, 8 },
                    { 55, 55, 59 },
                    { 54, 54, 49 },
                    { 53, 53, 90 },
                    { 74, 74, 16 },
                    { 52, 52, 52 },
                    { 75, 75, 94 },
                    { 77, 77, 8 },
                    { 98, 98, 46 },
                    { 97, 97, 2 },
                    { 96, 96, 65 },
                    { 95, 95, 27 },
                    { 94, 94, 88 },
                    { 93, 93, 29 },
                    { 92, 92, 97 },
                    { 91, 91, 90 },
                    { 90, 90, 28 },
                    { 89, 89, 3 },
                    { 88, 88, 72 },
                    { 87, 87, 0 },
                    { 86, 86, 11 },
                    { 85, 85, 10 },
                    { 84, 84, 85 },
                    { 83, 83, 55 },
                    { 82, 82, 23 },
                    { 81, 81, 18 },
                    { 80, 80, 37 },
                    { 79, 79, 11 },
                    { 78, 78, 91 },
                    { 76, 76, 35 },
                    { 51, 51, 15 },
                    { 50, 50, 47 },
                    { 49, 49, 72 },
                    { 22, 22, 71 },
                    { 21, 21, 2 },
                    { 20, 20, 79 },
                    { 19, 19, 73 },
                    { 18, 18, 21 },
                    { 17, 17, 71 },
                    { 16, 16, 10 },
                    { 15, 15, 32 },
                    { 14, 14, 30 },
                    { 13, 13, 81 },
                    { 12, 12, 54 },
                    { 11, 11, 80 },
                    { 10, 10, 40 },
                    { 9, 9, 41 },
                    { 8, 8, 97 },
                    { 7, 7, 47 },
                    { 6, 6, 39 },
                    { 5, 5, 11 },
                    { 4, 4, 27 },
                    { 3, 3, 30 },
                    { 2, 2, 46 },
                    { 23, 23, 11 },
                    { 24, 24, 20 },
                    { 25, 25, 92 },
                    { 26, 26, 75 },
                    { 48, 48, 86 },
                    { 47, 47, 56 },
                    { 46, 46, 54 },
                    { 45, 45, 9 },
                    { 44, 44, 19 },
                    { 43, 43, 62 },
                    { 42, 42, 66 },
                    { 41, 41, 85 },
                    { 40, 40, 56 },
                    { 39, 39, 24 },
                    { 99, 99, 42 },
                    { 38, 38, 64 },
                    { 36, 36, 25 },
                    { 35, 35, 36 },
                    { 34, 34, 63 },
                    { 33, 33, 22 },
                    { 32, 32, 91 },
                    { 31, 31, 63 },
                    { 30, 30, 26 },
                    { 29, 29, 33 },
                    { 28, 28, 28 },
                    { 27, 27, 10 },
                    { 37, 37, 6 },
                    { 100, 100, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                schema: "Catalog",
                table: "Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductInStockId",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductInStockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");
        }
    }
}
