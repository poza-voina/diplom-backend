using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "routeExampleRecords",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clientId = table.Column<long>(type: "bigint", nullable: false),
                    routeExampleId = table.Column<long>(type: "bigint", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routeExampleRecords", x => x.id);
                    table.ForeignKey(
                        name: "FK_routeExampleRecords_routeExamples_routeExampleId",
                        column: x => x.routeExampleId,
                        principalTable: "routeExamples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_routeExampleRecords_users_clientId",
                        column: x => x.clientId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_routeExampleRecords_clientId",
                table: "routeExampleRecords",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_routeExampleRecords_routeExampleId_clientId",
                table: "routeExampleRecords",
                columns: new[] { "routeExampleId", "clientId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "routeExampleRecords");
        }
    }
}
