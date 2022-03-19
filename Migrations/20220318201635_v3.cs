using Microsoft.EntityFrameworkCore.Migrations;

namespace wyyybbb.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vinyl_Prodavnice_prodavnicaID",
                table: "Vinyl");

            migrationBuilder.DropIndex(
                name: "IX_Vinyl_prodavnicaID",
                table: "Vinyl");

            migrationBuilder.DropColumn(
                name: "Tip",
                table: "Vinyl");

            migrationBuilder.DropColumn(
                name: "prodavnicaID",
                table: "Vinyl");

            migrationBuilder.DropColumn(
                name: "BrVinyl",
                table: "Prodavnice");

            migrationBuilder.DropColumn(
                name: "PIB",
                table: "IzdavackeKuce");

            migrationBuilder.CreateTable(
                name: "SpojProdavnicaPloca",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    prodavnicaID = table.Column<int>(type: "int", nullable: true),
                    plocaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpojProdavnicaPloca", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SpojProdavnicaPloca_Prodavnice_prodavnicaID",
                        column: x => x.prodavnicaID,
                        principalTable: "Prodavnice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpojProdavnicaPloca_Vinyl_plocaID",
                        column: x => x.plocaID,
                        principalTable: "Vinyl",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpojProdavnicaPloca_plocaID",
                table: "SpojProdavnicaPloca",
                column: "plocaID");

            migrationBuilder.CreateIndex(
                name: "IX_SpojProdavnicaPloca_prodavnicaID",
                table: "SpojProdavnicaPloca",
                column: "prodavnicaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpojProdavnicaPloca");

            migrationBuilder.AddColumn<int>(
                name: "Tip",
                table: "Vinyl",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "prodavnicaID",
                table: "Vinyl",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrVinyl",
                table: "Prodavnice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PIB",
                table: "IzdavackeKuce",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vinyl_prodavnicaID",
                table: "Vinyl",
                column: "prodavnicaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vinyl_Prodavnice_prodavnicaID",
                table: "Vinyl",
                column: "prodavnicaID",
                principalTable: "Prodavnice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
