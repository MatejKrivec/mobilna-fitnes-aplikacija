using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntervalTimerWebApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uporabniki",
                columns: table => new
                {
                    id_uporabnik = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ime = table.Column<string>(type: "TEXT", nullable: true),
                    priimek = table.Column<string>(type: "TEXT", nullable: true),
                    uporabnisko_ime = table.Column<string>(type: "TEXT", nullable: true),
                    eposta = table.Column<string>(type: "TEXT", nullable: true),
                    geslo = table.Column<string>(type: "TEXT", nullable: true),
                    datum_registracije = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uporabniki", x => x.id_uporabnik);
                });

            migrationBuilder.CreateTable(
                name: "Treningi",
                columns: table => new
                {
                    id_trening = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    datum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    trajanje = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    porabljene_kalorije = table.Column<int>(type: "INTEGER", nullable: false),
                    tk_uporabnik = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treningi", x => x.id_trening);
                    table.ForeignKey(
                        name: "FK_Treningi_Uporabniki_tk_uporabnik",
                        column: x => x.tk_uporabnik,
                        principalTable: "Uporabniki",
                        principalColumn: "id_uporabnik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vadbe",
                columns: table => new
                {
                    id_vadba = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    kalorije = table.Column<int>(type: "INTEGER", nullable: false),
                    work = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    rest = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    sets = table.Column<int>(type: "INTEGER", nullable: false),
                    fk_trening = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vadbe", x => x.id_vadba);
                    table.ForeignKey(
                        name: "FK_Vadbe_Treningi_fk_trening",
                        column: x => x.fk_trening,
                        principalTable: "Treningi",
                        principalColumn: "id_trening",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Treningi_tk_uporabnik",
                table: "Treningi",
                column: "tk_uporabnik");

            migrationBuilder.CreateIndex(
                name: "IX_Vadbe_fk_trening",
                table: "Vadbe",
                column: "fk_trening");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vadbe");

            migrationBuilder.DropTable(
                name: "Treningi");

            migrationBuilder.DropTable(
                name: "Uporabniki");
        }
    }
}
