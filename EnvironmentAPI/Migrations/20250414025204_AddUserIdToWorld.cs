using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvironmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToWorld : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Worlds_UserId",
                table: "Worlds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Worlds_Users_UserId",
                table: "Worlds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worlds_Users_UserId",
                table: "Worlds");

            migrationBuilder.DropIndex(
                name: "IX_Worlds_UserId",
                table: "Worlds");
        }
    }
}
