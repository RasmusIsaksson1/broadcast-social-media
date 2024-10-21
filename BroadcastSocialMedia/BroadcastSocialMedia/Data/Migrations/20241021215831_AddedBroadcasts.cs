using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BroadcastSocialMedia.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedBroadcasts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Broadcast_AspNetUsers_UserId",
                table: "Broadcast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Broadcast",
                table: "Broadcast");

            migrationBuilder.RenameTable(
                name: "Broadcast",
                newName: "Broadcasts");

            migrationBuilder.RenameIndex(
                name: "IX_Broadcast_UserId",
                table: "Broadcasts",
                newName: "IX_Broadcasts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Broadcasts",
                table: "Broadcasts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcasts_AspNetUsers_UserId",
                table: "Broadcasts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Broadcasts_AspNetUsers_UserId",
                table: "Broadcasts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Broadcasts",
                table: "Broadcasts");

            migrationBuilder.RenameTable(
                name: "Broadcasts",
                newName: "Broadcast");

            migrationBuilder.RenameIndex(
                name: "IX_Broadcasts_UserId",
                table: "Broadcast",
                newName: "IX_Broadcast_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Broadcast",
                table: "Broadcast",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcast_AspNetUsers_UserId",
                table: "Broadcast",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
