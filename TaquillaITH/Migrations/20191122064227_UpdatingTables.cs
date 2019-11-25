using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaquillaITH.Migrations
{
    public partial class UpdatingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheatreRooms_Sales_SaleId",
                table: "TheatreRooms");

            migrationBuilder.DropIndex(
                name: "IX_TheatreRooms_SaleId",
                table: "TheatreRooms");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "TheatreRooms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DaySales");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "DaySales");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Sales",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Schedule",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TheatreRoomId",
                table: "Movies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MovieId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    SaleId = table.Column<int>(nullable: true),
                    TheatreRoomId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seat_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seat_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seat_TheatreRooms_TheatreRoomId",
                        column: x => x.TheatreRoomId,
                        principalTable: "TheatreRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_MovieId",
                table: "Sales",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_TheatreRoomId",
                table: "Movies",
                column: "TheatreRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_MovieId",
                table: "Seat",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_SaleId",
                table: "Seat",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_TheatreRoomId",
                table: "Seat",
                column: "TheatreRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_TheatreRooms_TheatreRoomId",
                table: "Movies",
                column: "TheatreRoomId",
                principalTable: "TheatreRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Movies_MovieId",
                table: "Sales",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_TheatreRooms_TheatreRoomId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Movies_MovieId",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Sales_MovieId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Movies_TheatreRoomId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Schedule",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "TheatreRoomId",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "TheatreRooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DaySales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "DaySales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TheatreRooms_SaleId",
                table: "TheatreRooms",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TheatreRooms_Sales_SaleId",
                table: "TheatreRooms",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
