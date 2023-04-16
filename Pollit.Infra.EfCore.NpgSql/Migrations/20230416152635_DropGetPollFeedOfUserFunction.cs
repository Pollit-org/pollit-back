using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;
using Pollit.SeedWork;

#nullable disable

namespace Pollit.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class DropGetPollFeedOfUserFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = Assembly.GetExecutingAssembly().ReadResource("20230416152635_DropGetPollFeedOfUserFunction.Up.sql");
            migrationBuilder.Sql(sql);
            
            migrationBuilder.CreateIndex(
                name: "IX_Polls.Options.Votes_VoterId",
                table: "Polls.Options.Votes",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_AuthorId",
                table: "Polls",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_CreatedAt",
                table: "Polls",
                column: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = Assembly.GetExecutingAssembly().ReadResource("20230416152635_DropGetPollFeedOfUserFunction.Down.sql");
            migrationBuilder.Sql(sql);
            
            migrationBuilder.DropIndex(
                name: "IX_Polls.Options.Votes_VoterId",
                table: "Polls.Options.Votes");

            migrationBuilder.DropIndex(
                name: "IX_Polls_AuthorId",
                table: "Polls");

            migrationBuilder.DropIndex(
                name: "IX_Polls_CreatedAt",
                table: "Polls");
        }
    }
}
