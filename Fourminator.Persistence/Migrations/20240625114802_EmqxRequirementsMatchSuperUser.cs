using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourMinator.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EmqxRequirementsMatchSuperUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_superuser",
                table: "Robots",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_superuser",
                table: "Robots");
        }
    }
}
