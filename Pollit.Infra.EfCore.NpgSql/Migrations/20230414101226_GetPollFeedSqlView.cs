using System.Reflection;
using System.Resources;
using Microsoft.EntityFrameworkCore.Migrations;
using Pollit.SeedWork;

#nullable disable

namespace Pollit.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class GetPollFeedSqlView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = Assembly.GetExecutingAssembly().ReadResource("20230414101226_GetPollFeedSqlView.Create.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = Assembly.GetExecutingAssembly().ReadResource("20230414101226_GetPollFeedSqlView.Drop.sql");
            migrationBuilder.Sql(sql);
        }
    }
}
