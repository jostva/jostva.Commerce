using Microsoft.EntityFrameworkCore.Migrations;

namespace jostva.Commerce.Customer.Data.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "Customer",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Lastname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                schema: "Customer",
                table: "Clients",
                columns: new[] { "ClientId", "Lastname", "Name" },
                values: new object[,]
                {
                    { 1, null, "Client 1" },
                    { 2, null, "Client 2" },
                    { 3, null, "Client 3" },
                    { 4, null, "Client 4" },
                    { 5, null, "Client 5" },
                    { 6, null, "Client 6" },
                    { 7, null, "Client 7" },
                    { 8, null, "Client 8" },
                    { 9, null, "Client 9" },
                    { 10, null, "Client 10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients",
                schema: "Customer");
        }
    }
}
