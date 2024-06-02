using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourMinator.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedMatchTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlayerYellowId = table.Column<uint>(type: "int unsigned", nullable: false),
                    PlayerRedId = table.Column<uint>(type: "int unsigned", nullable: false),
                    RobotId = table.Column<uint>(type: "int unsigned", nullable: true),
                    WinnerId = table.Column<uint>(type: "int unsigned", nullable: true),
                    YellowStones = table.Column<short>(type: "smallint", nullable: false),
                    RedStones = table.Column<short>(type: "smallint", nullable: false),
                    State = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Players_PlayerRedId",
                        column: x => x.PlayerRedId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Players_PlayerYellowId",
                        column: x => x.PlayerYellowId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Players_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Robots_RobotId",
                        column: x => x.RobotId,
                        principalTable: "Robots",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerRedId",
                table: "Matches",
                column: "PlayerRedId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerYellowId",
                table: "Matches",
                column: "PlayerYellowId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_RobotId",
                table: "Matches",
                column: "RobotId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinnerId",
                table: "Matches",
                column: "WinnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
