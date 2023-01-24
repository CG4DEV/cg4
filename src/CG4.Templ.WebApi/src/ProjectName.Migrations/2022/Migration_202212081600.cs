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
                .WithColumn("password").AsString().NotNullable()
                .WithColumn("create_date").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("update_date").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);

            Insert.IntoTable("accounts")
                .Row(new Dictionary<string, object>
                {
                    ["login"] = "Test login",
                    ["password"] = "Test password",
                    ["create_date"] = DateTimeOffset.UtcNow,
                    ["update_date"] = DateTimeOffset.UtcNow
                })
                .Row(new Dictionary<string, object>
                {
                    ["login"] = "Some login",
                    ["password"] = "Some password",
                    ["create_date"] = DateTimeOffset.UtcNow,
                    ["update_date"] = DateTimeOffset.UtcNow
                })
                .Row(new Dictionary<string, object>
                {
                    ["login"] = "Login",
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
