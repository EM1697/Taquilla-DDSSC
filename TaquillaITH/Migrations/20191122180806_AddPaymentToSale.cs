using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaquillaITH.Migrations
{
    public partial class AddPaymentToSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaySalesId",
                table: "Sales",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Sales",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleDate",
                table: "Sales",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleDate",
                table: "DaySales",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DaySalesId",
                table: "Sales",
                column: "DaySalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PaymentId",
                table: "Sales",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_DaySales_DaySalesId",
                table: "Sales",
                column: "DaySalesId",
                principalTable: "DaySales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Payments_PaymentId",
                table: "Sales",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_DaySales_DaySalesId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Payments_PaymentId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_DaySalesId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_PaymentId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "DaySalesId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SaleDate",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SaleDate",
                table: "DaySales");
        }
    }
}
