using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_routes_title",
                table: "routes",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_routeCategories_title",
                table: "routeCategories",
                column: "title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_routes_title",
                table: "routes");

            migrationBuilder.DropIndex(
                name: "IX_routeCategories_title",
                table: "routeCategories");
        }
    }
}
