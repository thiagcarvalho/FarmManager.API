using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FarmManager.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddToque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Toques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cowId = table.Column<int>(type: "integer", nullable: false),
                    dataToque = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    vacaPrenha = table.Column<bool>(type: "boolean", nullable: false),
                    tempoGestacaoDias = table.Column<int>(type: "integer", nullable: false),
                    observacoes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Toques_Cows_cowId",
                        column: x => x.cowId,
                        principalTable: "Cows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Toques_cowId",
                table: "Toques",
                column: "cowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Toques");
        }
    }
}
