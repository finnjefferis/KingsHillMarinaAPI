using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingsHillMarinaAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_Berths_BerthId",
                table: "Boats");

            migrationBuilder.DropForeignKey(
                name: "FK_Boats_Owners_OwnerId",
                table: "Boats");

            migrationBuilder.DropIndex(
                name: "IX_Boats_BerthId",
                table: "Boats");

            migrationBuilder.DropIndex(
                name: "IX_Boats_OwnerId",
                table: "Boats");

            migrationBuilder.AddColumn<string>(
                name: "BoatIds",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "BoatId",
                table: "Berths",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoatIds",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "BoatId",
                table: "Berths");

            migrationBuilder.CreateIndex(
                name: "IX_Boats_BerthId",
                table: "Boats",
                column: "BerthId",
                unique: true,
                filter: "[BerthId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Boats_OwnerId",
                table: "Boats",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_Berths_BerthId",
                table: "Boats",
                column: "BerthId",
                principalTable: "Berths",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_Owners_OwnerId",
                table: "Boats",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
