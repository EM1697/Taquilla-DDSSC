using Microsoft.EntityFrameworkCore.Migrations;

namespace TaquillaITH.Migrations
{
    public partial class ShowSeats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsedSeats",
                table: "Shows",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedSeats",
                table: "Shows");
        }
    }
}
