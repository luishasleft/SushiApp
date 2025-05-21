using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SushiApp.Migrations
{
    /// <inheritdoc />
    public partial class ordiniaggiunta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataOrdine = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Stato = table.Column<string>(type: "TEXT", nullable: false),
                    Tavolo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdineDettagli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PiattoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantita = table.Column<int>(type: "INTEGER", nullable: false),
                    OrdineId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdineDettagli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdineDettagli_Ordini_OrdineId",
                        column: x => x.OrdineId,
                        principalTable: "Ordini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdineDettagli_Piatti_PiattoId",
                        column: x => x.PiattoId,
                        principalTable: "Piatti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdineDettagli_OrdineId",
                table: "OrdineDettagli",
                column: "OrdineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdineDettagli_PiattoId",
                table: "OrdineDettagli",
                column: "PiattoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdineDettagli");

            migrationBuilder.DropTable(
                name: "Ordini");
        }
    }
}
