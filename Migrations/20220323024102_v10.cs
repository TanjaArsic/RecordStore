using Microsoft.EntityFrameworkCore.Migrations;

namespace wyyybbb.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vinyl_IzdavackeKuce_izdavackaKucaID",
                table: "Vinyl");

            migrationBuilder.DropTable(
                name: "IzdavackeKuce");

            migrationBuilder.DropIndex(
                name: "IX_Vinyl_izdavackaKucaID",
                table: "Vinyl");

            migrationBuilder.DropColumn(
                name: "izdavackaKucaID",
                table: "Vinyl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "izdavackaKucaID",
                table: "Vinyl",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IzdavackeKuce",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzdavackeKuce", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vinyl_izdavackaKucaID",
                table: "Vinyl",
                column: "izdavackaKucaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vinyl_IzdavackeKuce_izdavackaKucaID",
                table: "Vinyl",
                column: "izdavackaKucaID",
                principalTable: "IzdavackeKuce",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
