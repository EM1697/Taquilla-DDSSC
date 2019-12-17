using Microsoft.EntityFrameworkCore.Migrations;

namespace TaquillaITH.Migrations
{
    public partial class removeDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
