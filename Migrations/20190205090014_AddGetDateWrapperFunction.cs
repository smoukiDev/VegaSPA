using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaSPA.Migrations
{
    public partial class AddGetDateWrapperFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string createFunctionScript = "CREATE FUNCTION [dbo].[GETCURRENTDATE] "
                                        + "() "
                                        + "RETURNS DATETIME "
                                        + "AS "
                                        + "BEGIN "
                                        + "RETURN GETUTCDATE() "
                                        + "END";
            migrationBuilder.Sql(createFunctionScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string dropFunctionScript = "DROP FUNCTION [dbo].[GETCURRENTDATE]";
            migrationBuilder.Sql(dropFunctionScript);
        }
    }
}
