using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pollit.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class RenameBirthdayToBirthdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Users",
                newName: "Birthdate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Users",
                newName: "Birthday");
        }
    }
}
