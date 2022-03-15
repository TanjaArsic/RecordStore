using Microsoft.EntityFrameworkCore.Migrations;

namespace wyyybbb.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prodata",
                table: "Vinyl");

            migrationBuilder.AddColumn<string>(
                name: "Pesme",
                table: "Vinyl",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Zanr",
                table: "Vinyl",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pesme",
                table: "Vinyl");

            migrationBuilder.DropColumn(
                name: "Zanr",
                table: "Vinyl");

            migrationBuilder.AddColumn<bool>(
                name: "Prodata",
                table: "Vinyl",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
