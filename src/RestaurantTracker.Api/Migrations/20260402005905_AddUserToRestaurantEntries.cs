using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToRestaurantEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RestaurantEntries",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantEntries_UserId",
                table: "RestaurantEntries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantEntries_AspNetUsers_UserId",
                table: "RestaurantEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantEntries_AspNetUsers_UserId",
                table: "RestaurantEntries");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantEntries_UserId",
                table: "RestaurantEntries");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RestaurantEntries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
