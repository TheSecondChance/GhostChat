using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostChat.Data.Migrations
{
    public partial class FriendshipStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AreFriends",
                table: "Friendships",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreFriends",
                table: "Friendships");
        }
    }
}
