using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourMinator.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedStonesFromMatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedStones",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "YellowStones",
                table: "Matches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "RedStones",
                table: "Matches",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "YellowStones",
                table: "Matches",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
