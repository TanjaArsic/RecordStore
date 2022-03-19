using Microsoft.EntityFrameworkCore.Migrations;

namespace wyyybbb.Migrations
{
    public partial class v3cls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpojProdavnicaPloca_Prodavnice_prodavnicaID",
                table: "SpojProdavnicaPloca");

            migrationBuilder.DropForeignKey(
                name: "FK_SpojProdavnicaPloca_Vinyl_plocaID",
                table: "SpojProdavnicaPloca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpojProdavnicaPloca",
                table: "SpojProdavnicaPloca");

            migrationBuilder.RenameTable(
                name: "SpojProdavnicaPloca",
                newName: "ProdavnicaPloca");

            migrationBuilder.RenameIndex(
                name: "IX_SpojProdavnicaPloca_prodavnicaID",
                table: "ProdavnicaPloca",
                newName: "IX_ProdavnicaPloca_prodavnicaID");

            migrationBuilder.RenameIndex(
                name: "IX_SpojProdavnicaPloca_plocaID",
                table: "ProdavnicaPloca",
                newName: "IX_ProdavnicaPloca_plocaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdavnicaPloca",
                table: "ProdavnicaPloca",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdavnicaPloca_Prodavnice_prodavnicaID",
                table: "ProdavnicaPloca",
                column: "prodavnicaID",
                principalTable: "Prodavnice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdavnicaPloca_Vinyl_plocaID",
                table: "ProdavnicaPloca",
                column: "plocaID",
                principalTable: "Vinyl",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdavnicaPloca_Prodavnice_prodavnicaID",
                table: "ProdavnicaPloca");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdavnicaPloca_Vinyl_plocaID",
                table: "ProdavnicaPloca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdavnicaPloca",
                table: "ProdavnicaPloca");

            migrationBuilder.RenameTable(
                name: "ProdavnicaPloca",
                newName: "SpojProdavnicaPloca");

            migrationBuilder.RenameIndex(
                name: "IX_ProdavnicaPloca_prodavnicaID",
                table: "SpojProdavnicaPloca",
                newName: "IX_SpojProdavnicaPloca_prodavnicaID");

            migrationBuilder.RenameIndex(
                name: "IX_ProdavnicaPloca_plocaID",
                table: "SpojProdavnicaPloca",
                newName: "IX_SpojProdavnicaPloca_plocaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpojProdavnicaPloca",
                table: "SpojProdavnicaPloca",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SpojProdavnicaPloca_Prodavnice_prodavnicaID",
                table: "SpojProdavnicaPloca",
                column: "prodavnicaID",
                principalTable: "Prodavnice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpojProdavnicaPloca_Vinyl_plocaID",
                table: "SpojProdavnicaPloca",
                column: "plocaID",
                principalTable: "Vinyl",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
