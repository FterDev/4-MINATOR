using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourMinator.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EmqxRequirementsMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Robots",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Robots",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Thumbprint",
                table: "Robots",
                newName: "salt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Robots",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Robots",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "salt",
                table: "Robots",
                newName: "Thumbprint");
        }
    }
}
