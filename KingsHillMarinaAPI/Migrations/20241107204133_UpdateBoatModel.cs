using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingsHillMarinaAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBoatModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Berths_Boats_BoatId",
                table: "Berths");

            migrationBuilder.DropIndex(
                name: "IX_Berths_BoatId",
                table: "Berths");

            migrationBuilder.DropColumn(
                name: "BoatId",
                table: "Berths");

            migrationBuilder.AddColumn<int>(
                name: "BerthId",
                table: "Boats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boats_BerthId",
                table: "Boats",
                column: "BerthId",
                unique: true,
                filter: "[BerthId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_Berths_BerthId",
                table: "Boats",
                column: "BerthId",
                principalTable: "Berths",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_Berths_BerthId",
                table: "Boats");

            migrationBuilder.DropIndex(
                name: "IX_Boats_BerthId",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "BerthId",
                table: "Boats");

            migrationBuilder.AddColumn<int>(
                name: "BoatId",
                table: "Berths",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Berths_BoatId",
                table: "Berths",
                column: "BoatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Berths_Boats_BoatId",
                table: "Berths",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id");
        }
    }
}
