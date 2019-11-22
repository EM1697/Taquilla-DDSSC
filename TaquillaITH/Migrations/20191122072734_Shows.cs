using Microsoft.EntityFrameworkCore.Migrations;

namespace TaquillaITH.Migrations
{
    public partial class Shows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Show_Movies_MovieId",
                table: "Show");

            migrationBuilder.DropForeignKey(
                name: "FK_Show_TheatreRooms_TheatreRoomId",
                table: "Show");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Show",
                table: "Show");

            migrationBuilder.RenameTable(
                name: "Show",
                newName: "Shows");

            migrationBuilder.RenameIndex(
                name: "IX_Show_TheatreRoomId",
                table: "Shows",
                newName: "IX_Shows_TheatreRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Show_MovieId",
                table: "Shows",
                newName: "IX_Shows_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shows",
                table: "Shows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Movies_MovieId",
                table: "Shows",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_TheatreRooms_TheatreRoomId",
                table: "Shows",
                column: "TheatreRoomId",
                principalTable: "TheatreRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Movies_MovieId",
                table: "Shows");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_TheatreRooms_TheatreRoomId",
                table: "Shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shows",
                table: "Shows");

            migrationBuilder.RenameTable(
                name: "Shows",
                newName: "Show");

            migrationBuilder.RenameIndex(
                name: "IX_Shows_TheatreRoomId",
                table: "Show",
                newName: "IX_Show_TheatreRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Shows_MovieId",
                table: "Show",
                newName: "IX_Show_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Show",
                table: "Show",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Show_Movies_MovieId",
                table: "Show",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Show_TheatreRooms_TheatreRoomId",
                table: "Show",
                column: "TheatreRoomId",
                principalTable: "TheatreRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
