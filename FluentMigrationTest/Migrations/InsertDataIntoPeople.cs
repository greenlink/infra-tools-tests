using FluentMigrator;

namespace FluentMigrationTest.Migrations
{
    [Migration(202204281602)]
    public class InsertDataIntoPeople : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("People").Row(new { Name = "Lennon" });
        }

        public override void Down()
        {
            Delete.FromTable("People").Row(new { Name = "Lennon" });
        }
    }
}