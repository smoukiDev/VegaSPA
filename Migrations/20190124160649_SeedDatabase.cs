using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

namespace VegaSPA.Migrations
{
    public partial class SeedDatabase : Migration
    {    
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            List<string> makes = this.InitializeMakes();
            List<string> models = this.InitializeModels();

            foreach (var make in makes)
            {
                migrationBuilder.Sql($"INSERT INTO Makes (Name) VALUES ('{make}')");
            }

            foreach (var model in models)
            {
                string subQuery = $"SELECT ID FROM Makes WHERE Name = '{model.Split('-').GetValue(0)}'";
                migrationBuilder.Sql($"INSERT INTO Models (Name, MakeId) VALUES ('{model}', ({subQuery}))");              
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            List<string> makes = this.InitializeMakes();
            foreach (var make in makes)
            {
                migrationBuilder.Sql($"DELETE FROM Makes WHERE Name = '{make}' ");
            }            
        }
        
        private List<string> InitializeMakes()
        {
            var makes = new List<string>()
            {
                "Make1",
                "Make2",
                "Make3"
            };

            return makes;                     
        }

        private List<string> InitializeModels()
        {
            var models = new List<string>()
            {
                "Make1-ModelA",
                "Make1-ModelB",
                "Make1-ModelC",
                "Make2-ModelA",
                "Make2-ModelB",
                "Make2-ModelC",
                "Make3-ModelA",
                "Make3-ModelB",
                "Make3-ModelC"
            };

            return models;                     
        }        
    }
}
