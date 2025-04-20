using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteRouteCategory_routeCategories_RouteCategoriesId",
                table: "RouteRouteCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteRouteCategory_routes_RoutesId",
                table: "RouteRouteCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RouteRouteCategory",
                table: "RouteRouteCategory");

            migrationBuilder.RenameTable(
                name: "RouteRouteCategory",
                newName: "routeRouteCategory");

            migrationBuilder.RenameColumn(
                name: "RoutesId",
                table: "routeRouteCategory",
                newName: "RouteCategoryId");

            migrationBuilder.RenameColumn(
                name: "RouteCategoriesId",
                table: "routeRouteCategory",
                newName: "RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_RouteRouteCategory_RoutesId",
                table: "routeRouteCategory",
                newName: "IX_routeRouteCategory_RouteCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_routeRouteCategory",
                table: "routeRouteCategory",
                columns: new[] { "RouteId", "RouteCategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_routeRouteCategory_routeCategories_RouteCategoryId",
                table: "routeRouteCategory",
                column: "RouteCategoryId",
                principalTable: "routeCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_routeRouteCategory_routes_RouteId",
                table: "routeRouteCategory",
                column: "RouteId",
                principalTable: "routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_routeRouteCategory_routeCategories_RouteCategoryId",
                table: "routeRouteCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_routeRouteCategory_routes_RouteId",
                table: "routeRouteCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_routeRouteCategory",
                table: "routeRouteCategory");

            migrationBuilder.RenameTable(
                name: "routeRouteCategory",
                newName: "RouteRouteCategory");

            migrationBuilder.RenameColumn(
                name: "RouteCategoryId",
                table: "RouteRouteCategory",
                newName: "RoutesId");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "RouteRouteCategory",
                newName: "RouteCategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_routeRouteCategory_RouteCategoryId",
                table: "RouteRouteCategory",
                newName: "IX_RouteRouteCategory_RoutesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RouteRouteCategory",
                table: "RouteRouteCategory",
                columns: new[] { "RouteCategoriesId", "RoutesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RouteRouteCategory_routeCategories_RouteCategoriesId",
                table: "RouteRouteCategory",
                column: "RouteCategoriesId",
                principalTable: "routeCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteRouteCategory_routes_RoutesId",
                table: "RouteRouteCategory",
                column: "RoutesId",
                principalTable: "routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
