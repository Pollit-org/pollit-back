using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pollit.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserBirthdateAsString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Birthdate",
                table: "Users",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Birthdate",table: "Users");
        }
    }
}
