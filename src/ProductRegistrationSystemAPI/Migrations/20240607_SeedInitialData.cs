using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductRegistrationSystemAPI.Migrations;

public partial class SeedInitialData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        for (int i = 1; i <= 100; i++)
        {
            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "CatalogId", "Name" },
                values: new object?[] { (long)i, $"Catalog {i}" }
            );
        }

        for (int i = 1; i <= 100; i++)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Name", "Email" },
                values: new object?[] { (long)i, $"Customer {i}", $"customer{i}@example.com" }
            );
        }

        for (int i = 1; i <= 100; i++)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Name", "Status", "Stock", "Description", "Price" },
                values: new object?[] { (long)i, $"Product {i}", (byte)1, i * 10, $"Description {i}", (decimal)(i * 1.5) }
            );
        }
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        for (int i = 1; i <= 100; i++)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: (long)i
            );

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: (long)i
            );

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: (long)i
            );
        }
    }
}
