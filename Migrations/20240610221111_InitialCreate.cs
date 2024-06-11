using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebScrapping_C.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    scientific_name = table.Column<string>(type: "text", nullable: true),
                    group = table.Column<string>(type: "text", nullable: true),
                    brand = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    units = table.Column<string>(type: "text", nullable: true),
                    value_per_100_g = table.Column<string>(type: "text", nullable: true),
                    standard_daviation = table.Column<string>(type: "text", nullable: true),
                    minimum_value = table.Column<string>(type: "text", nullable: true),
                    maximum_value = table.Column<string>(type: "text", nullable: true),
                    number_of_data_used = table.Column<string>(type: "text", nullable: true),
                    references = table.Column<string>(type: "text", nullable: true),
                    data_type = table.Column<string>(type: "text", nullable: true),
                    component = table.Column<string>(type: "text", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_properties", x => x.id);
                    table.ForeignKey(
                        name: "FK_properties_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_properties_ItemId",
                table: "properties",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "properties");

            migrationBuilder.DropTable(
                name: "items");
        }
    }
}
