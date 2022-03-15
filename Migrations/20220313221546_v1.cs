using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wyyybbb.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IzdavackeKuce",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GodinaNastanka = table.Column<int>(type: "int", nullable: false),
                    PIB = table.Column<int>(type: "int", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzdavackeKuce", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Izvodjaci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izvodjaci", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prodavnice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BrVinyl = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavnice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prodavci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BrojTelefona = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LicnaKarta = table.Column<int>(type: "int", nullable: false),
                    RadnoVreme = table.Column<int>(type: "int", nullable: false),
                    prodavnicaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavci", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prodavci_Prodavnice_prodavnicaID",
                        column: x => x.prodavnicaID,
                        principalTable: "Prodavnice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vinyl",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    Prodata = table.Column<bool>(type: "bit", nullable: false),
                    GodinaStampanja = table.Column<int>(type: "int", nullable: false),
                    izvodjacID = table.Column<int>(type: "int", nullable: true),
                    izdavackaKucaID = table.Column<int>(type: "int", nullable: true),
                    prodavnicaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vinyl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vinyl_IzdavackeKuce_izdavackaKucaID",
                        column: x => x.izdavackaKucaID,
                        principalTable: "IzdavackeKuce",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vinyl_Izvodjaci_izvodjacID",
                        column: x => x.izvodjacID,
                        principalTable: "Izvodjaci",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vinyl_Prodavnice_prodavnicaID",
                        column: x => x.prodavnicaID,
                        principalTable: "Prodavnice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prodavci_prodavnicaID",
                table: "Prodavci",
                column: "prodavnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vinyl_izdavackaKucaID",
                table: "Vinyl",
                column: "izdavackaKucaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vinyl_izvodjacID",
                table: "Vinyl",
                column: "izvodjacID");

            migrationBuilder.CreateIndex(
                name: "IX_Vinyl_prodavnicaID",
                table: "Vinyl",
                column: "prodavnicaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prodavci");

            migrationBuilder.DropTable(
                name: "Vinyl");

            migrationBuilder.DropTable(
                name: "IzdavackeKuce");

            migrationBuilder.DropTable(
                name: "Izvodjaci");

            migrationBuilder.DropTable(
                name: "Prodavnice");
        }
    }
}
