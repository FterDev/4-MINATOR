using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourMinator.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAttributesMatchAndAddedMatchMoves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AbortedAt",
                table: "Matches",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Matches",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedAt",
                table: "Matches",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedAt",
                table: "Matches",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MatchMoves",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MoveNumber = table.Column<short>(type: "smallint", nullable: false),
                    X = table.Column<short>(type: "smallint", nullable: false),
                    Y = table.Column<short>(type: "smallint", nullable: false),
                    PlayerId = table.Column<uint>(type: "int unsigned", nullable: false),
                    Color = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Skipped = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Joker = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MoveTime = table.Column<uint>(type: "int unsigned", nullable: false),
                    MoveTimestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchMoves", x => new { x.MatchId, x.MoveNumber });
                    table.ForeignKey(
                        name: "FK_MatchMoves_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchMoves_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MatchMoves_PlayerId",
                table: "MatchMoves",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchMoves");

            migrationBuilder.DropColumn(
                name: "AbortedAt",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "FinishedAt",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "StartedAt",
                table: "Matches");
        }
    }
}
