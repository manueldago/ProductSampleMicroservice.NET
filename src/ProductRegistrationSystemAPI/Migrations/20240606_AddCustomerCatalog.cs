using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductRegistrationSystemAPI.Migrations;

public partial class AddCustomerCatalog : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Catalogs",
            columns: table => new
            {
                CatalogId = table.Column<long>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Catalogs", x => x.CatalogId); });

        migrationBuilder.CreateTable(
            name: "Customers",
            columns: table => new
            {
                CustomerId = table.Column<long>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(nullable: true),
                Email = table.Column<string>(nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Customers", x => x.CustomerId); });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Catalogs");
        migrationBuilder.DropTable(name: "Customers");
    }
}
