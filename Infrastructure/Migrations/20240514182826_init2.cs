using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFiles_Users_UserId",
                table: "AppFiles");

            migrationBuilder.DropIndex(
                name: "IX_AppFiles_UserId",
                table: "AppFiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppFiles_UserId",
                table: "AppFiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFiles_Users_UserId",
                table: "AppFiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
