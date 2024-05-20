using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_projecttask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_TaskColumns_ColumnId",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_ColumnId",
                table: "ProjectTasks");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskColumnId",
                table: "ProjectTasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ProjectTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_TaskColumnId",
                table: "ProjectTasks",
                column: "TaskColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_TaskColumns_TaskColumnId",
                table: "ProjectTasks",
                column: "TaskColumnId",
                principalTable: "TaskColumns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_TaskColumns_TaskColumnId",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_TaskColumnId",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "TaskColumnId",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectTasks");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ColumnId",
                table: "ProjectTasks",
                column: "ColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_TaskColumns_ColumnId",
                table: "ProjectTasks",
                column: "ColumnId",
                principalTable: "TaskColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
