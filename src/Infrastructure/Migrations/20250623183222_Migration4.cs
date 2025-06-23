using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "admin",
                keyColumn: "id",
                keyValue: 1L,
                column: "email",
                value: "admin@tourguide.site");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "admin",
                keyColumn: "id",
                keyValue: 1L,
                column: "email",
                value: "admin@digitaldiary.site");
        }
    }
}
