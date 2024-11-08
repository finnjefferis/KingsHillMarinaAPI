using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingsHillMarinaAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boats_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Berths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Berths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Berths_Boats_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoatId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingRecords_Boats_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Berths_BoatId",
                table: "Berths",
                column: "BoatId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingRecords_BoatId",
                table: "BillingRecords",
                column: "BoatId");

            migrationBuilder.CreateIndex(
                name: "IX_Boats_OwnerId",
                table: "Boats",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Berths");

            migrationBuilder.DropTable(
                name: "BillingRecords");

            migrationBuilder.DropTable(
                name: "Boats");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
