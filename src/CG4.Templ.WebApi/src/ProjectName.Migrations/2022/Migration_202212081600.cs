using System;
using System.Collections.Generic;
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
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("password").AsString().NotNullable()
                .WithColumn("create_date").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("update_date").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);

            Insert.IntoTable("accounts")
                .Row(new Dictionary<string, object>
                {
                    ["login"] = "Test login",
                    ["name"] = "First user",
                    ["password"] = "Test password",
                    ["create_date"] = DateTimeOffset.UtcNow,
                    ["update_date"] = DateTimeOffset.UtcNow
                })
                .Row(new Dictionary<string, object>
                {
                    ["login"] = "Some login",
                    ["name"] = "Second user",
                    ["password"] = "Some password",
                    ["create_date"] = DateTimeOffset.UtcNow,
                    ["update_date"] = DateTimeOffset.UtcNow
                })
                .Row(new Dictionary<string, object>
                {
                    ["login"] = "Login",
                    ["name"] = "Third user",
                    ["password"] = "Password",
                    ["create_date"] = DateTimeOffset.UtcNow,
                    ["update_date"] = DateTimeOffset.UtcNow
                });
        }

        public override void Down()
        {
            Delete.Table("accounts");
        }
    }
}
