using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourMinator.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedBotLevelToMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ushort>(
                name: "BotLevel",
                table: "Matches",
                type: "smallint unsigned",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BotLevel",
                table: "Matches");
        }
    }
}
