using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaSPA.Migrations
{
    public partial class AddFeaturesStaticData : Migration
    {
        private readonly List<string> _features = new List<string>()
        {
            "Feature1",
            "Feature2",
            "Feature3",
            "Feature4",
            "Feature5"
        };
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var feature in _features)
            {
                 migrationBuilder.Sql($"INSERT INTO Features (Name) VALUES ('{feature}')"); 
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var feature in _features)
            {
                 migrationBuilder.Sql($"DELETE FROM Features WHERE Name = '{feature}' ");
            }
        }
    }
}
