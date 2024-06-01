using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourMinator.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatedRobotsSetNewFKId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Robots_Users_CreatedById",
                table: "Robots");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Robots",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Robots_CreatedById",
                table: "Robots",
                newName: "IX_Robots_CreatedByUserId");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Robots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Robots_Users_CreatedByUserId",
                table: "Robots",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Robots_Users_CreatedByUserId",
                table: "Robots");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Robots");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Robots",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Robots_CreatedByUserId",
                table: "Robots",
                newName: "IX_Robots_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Robots_Users_CreatedById",
                table: "Robots",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
