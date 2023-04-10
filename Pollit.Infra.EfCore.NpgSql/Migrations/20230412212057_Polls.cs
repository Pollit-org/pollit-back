using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pollit.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Polls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(125)", maxLength: 125, nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Polls.Options",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    Title = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: false),
                    PollId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls.Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polls.Options_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Polls.Options.Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    VoterId = table.Column<Guid>(type: "uuid", nullable: false),
                    VotedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PollOptionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls.Options.Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polls.Options.Votes_Polls.Options_PollOptionId",
                        column: x => x.PollOptionId,
                        principalTable: "Polls.Options",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Polls.Options_PollId",
                table: "Polls.Options",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls.Options.Votes_PollOptionId",
                table: "Polls.Options.Votes",
                column: "PollOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Polls.Options.Votes");

            migrationBuilder.DropTable(
                name: "Polls.Options");

            migrationBuilder.DropTable(
                name: "Polls");
        }
    }
}
