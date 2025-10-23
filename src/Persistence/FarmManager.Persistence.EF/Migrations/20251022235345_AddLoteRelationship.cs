using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FarmManager.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddLoteRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoteId",
                table: "Animals",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_LoteId",
                table: "Animals",
                column: "LoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_Name",
                table: "Lotes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Lotes_LoteId",
                table: "Animals",
                column: "LoteId",
                principalTable: "Lotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Lotes_LoteId",
                table: "Animals");

            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropIndex(
                name: "IX_Animals_LoteId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "LoteId",
                table: "Animals");
        }
    }
}
