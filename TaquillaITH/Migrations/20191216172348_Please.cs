using Microsoft.EntityFrameworkCore.Migrations;

namespace TaquillaITH.Migrations
{
    public partial class Please : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Movies");
        }
    }
}
