using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rename_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Project_ProjectId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_AppFiles_FileId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Users_CreatorId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Project_ProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskColumns_Dashboard_DashboardId",
                table: "TaskColumns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dashboard",
                table: "Dashboard");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameTable(
                name: "Dashboard",
                newName: "Dashboards");

            migrationBuilder.RenameIndex(
                name: "IX_Project_FileId",
                table: "Projects",
                newName: "IX_Projects_FileId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_CreatorId",
                table: "Projects",
                newName: "IX_Projects_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dashboards",
                table: "Dashboards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Projects_ProjectId",
                table: "Messages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AppFiles_FileId",
                table: "Projects",
                column: "FileId",
                principalTable: "AppFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_CreatorId",
                table: "Projects",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskColumns_Dashboards_DashboardId",
                table: "TaskColumns",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Projects_ProjectId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AppFiles_FileId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatorId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskColumns_Dashboards_DashboardId",
                table: "TaskColumns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dashboards",
                table: "Dashboards");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "Dashboards",
                newName: "Dashboard");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_FileId",
                table: "Project",
                newName: "IX_Project_FileId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CreatorId",
                table: "Project",
                newName: "IX_Project_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dashboard",
                table: "Dashboard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Project_ProjectId",
                table: "Messages",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AppFiles_FileId",
                table: "Project",
                column: "FileId",
                principalTable: "AppFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Users_CreatorId",
                table: "Project",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Project_ProjectId",
                table: "ProjectMembers",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskColumns_Dashboard_DashboardId",
                table: "TaskColumns",
                column: "DashboardId",
                principalTable: "Dashboard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
