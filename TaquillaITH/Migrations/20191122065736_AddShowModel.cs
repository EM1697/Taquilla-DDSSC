using Microsoft.EntityFrameworkCore.Migrations;

namespace TaquillaITH.Migrations
{
    public partial class AddShowModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_TheatreRooms_TheatreRoomId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Movies_MovieId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_MovieId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Movies_TheatreRoomId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "TheatreRoomId",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Seat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TheatreRoomId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seat_MovieId",
                table: "Seat",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_TheatreRoomId",
                table: "Movies",
                column: "TheatreRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_TheatreRooms_TheatreRoomId",
                table: "Movies",
                column: "TheatreRoomId",
                principalTable: "TheatreRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Movies_MovieId",
                table: "Seat",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
