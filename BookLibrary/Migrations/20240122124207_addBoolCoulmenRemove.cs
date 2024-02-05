using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    public partial class addBoolCoulmenRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ruternd",
                table: "books");

            migrationBuilder.AddColumn<bool>(
                name: "Ruternd",
                table: "rentedBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ruternd",
                table: "rentedBooks");

            migrationBuilder.AddColumn<bool>(
                name: "Ruternd",
                table: "books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
