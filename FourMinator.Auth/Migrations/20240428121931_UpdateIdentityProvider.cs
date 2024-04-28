using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourMinator.Auth.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentityProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "IdentityProviders");

            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "IdentityProviders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "IdentityProviders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "IdentityProviders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
