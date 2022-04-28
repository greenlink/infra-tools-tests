using FluentMigrator;

namespace FluentMigrationTest.Migrations
{
    [Migration(202204281018)]
    public class AddTablePeople : Migration
    {
        public override void Up() =>
            Create.Table("People")
                .WithColumn("IdPeople").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100)
                .WithColumn("Created").AsTime().WithDefault(SystemMethods.CurrentDateTime);

        public override void Down() => Delete.Table("People");
    }
}
