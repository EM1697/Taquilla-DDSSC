using Microsoft.EntityFrameworkCore.Migrations;

namespace TaquillaITH.Migrations
{
    public partial class FuckTheSeats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Sales_SaleId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_SaleId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Seat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "Seat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seat_SaleId",
                table: "Seat",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Sales_SaleId",
                table: "Seat",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
