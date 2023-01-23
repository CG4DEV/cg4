using FluentMigrator;

namespace ProjectName.Migrations._2022
{
    [Migration(202212081600)]
    public class Migration_202212081600 : Migration
    {
        public override void Up()
        {
            Create.Table("accounts")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("login").AsString().NotNullable()
                .WithColumn("password").AsString().NotNullable()
                .WithColumn("create_date").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("update_date").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("accounts");
        }
    }
}
