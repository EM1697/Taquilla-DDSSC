using Microsoft.EntityFrameworkCore.Migrations;

namespace TaquillaITH.Migrations
{
    public partial class cagada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Tickets3DVIPAmount",
                table: "DaySales",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TicketsVIPCount",
                table: "DaySales",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tickets3DVIPAmount",
                table: "DaySales");

            migrationBuilder.DropColumn(
                name: "TicketsVIPCount",
                table: "DaySales");
        }
    }
}
