using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntervalTimerWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVadba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vadbe_Treningi_fk_trening",
                table: "Vadbe");

            migrationBuilder.RenameColumn(
                name: "fk_trening",
                table: "Vadbe",
                newName: "tk_trening");

            migrationBuilder.RenameIndex(
                name: "IX_Vadbe_fk_trening",
                table: "Vadbe",
                newName: "IX_Vadbe_tk_trening");

            migrationBuilder.AddColumn<string>(
                name: "naziv",
                table: "Vadbe",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Vadbe_Treningi_tk_trening",
                table: "Vadbe",
                column: "tk_trening",
                principalTable: "Treningi",
                principalColumn: "id_trening",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vadbe_Treningi_tk_trening",
                table: "Vadbe");

            migrationBuilder.DropColumn(
                name: "naziv",
                table: "Vadbe");

            migrationBuilder.RenameColumn(
                name: "tk_trening",
                table: "Vadbe",
                newName: "fk_trening");

            migrationBuilder.RenameIndex(
                name: "IX_Vadbe_tk_trening",
                table: "Vadbe",
                newName: "IX_Vadbe_fk_trening");

            migrationBuilder.AddForeignKey(
                name: "FK_Vadbe_Treningi_fk_trening",
                table: "Vadbe",
                column: "fk_trening",
                principalTable: "Treningi",
                principalColumn: "id_trening",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
