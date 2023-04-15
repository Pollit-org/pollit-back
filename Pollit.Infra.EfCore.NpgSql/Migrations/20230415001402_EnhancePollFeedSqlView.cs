using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;
using Pollit.SeedWork;

#nullable disable

namespace Pollit.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class EnhancePollFeedSqlView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = Assembly.GetExecutingAssembly().ReadResource("20230415001402_EnhancePollFeedSqlView.Up.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = Assembly.GetExecutingAssembly().ReadResource("20230415001402_EnhancePollFeedSqlView.Down.sql");
            migrationBuilder.Sql(sql);
        }
    }
}
