using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaSPA.Migrations
{
    public partial class AddLastModifiedDefaultConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Vehicles",
                nullable: false,
                defaultValueSql: "[dbo].[getcurrentdate]()",
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "[dbo].[getcurrentdate]()");
        }
    }
}
