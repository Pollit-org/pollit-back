using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;
using Pollit.SeedWork;

#nullable disable

namespace Pollit.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class GetPollFeedOfUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = Assembly.GetExecutingAssembly().ReadResource("20230416140400_GetPollFeedOfUser.Up.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = Assembly.GetExecutingAssembly().ReadResource("20230416140400_GetPollFeedOfUser.Down.sql");
            migrationBuilder.Sql(sql);
        }
    }
}
