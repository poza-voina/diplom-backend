using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "routeCategories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routeCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RouteRouteCategory",
                columns: table => new
                {
                    RouteCategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    RoutesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteRouteCategory", x => new { x.RouteCategoriesId, x.RoutesId });
                    table.ForeignKey(
                        name: "FK_RouteRouteCategory_routeCategories_RouteCategoriesId",
                        column: x => x.RouteCategoriesId,
                        principalTable: "routeCategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteRouteCategory_routes_RoutesId",
                        column: x => x.RoutesId,
                        principalTable: "routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteRouteCategory_RoutesId",
                table: "RouteRouteCategory",
                column: "RoutesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteRouteCategory");

            migrationBuilder.DropTable(
                name: "routeCategories");
        }
    }
}
