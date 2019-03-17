namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreferenceUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Preferences_Id", "dbo.UserPreferences");
            DropIndex("dbo.AspNetUsers", new[] { "Preferences_Id" });
            DropPrimaryKey("dbo.UserPreferences");
            AddColumn("dbo.AspNetUsers", "Preferences_Preference", c => c.Int());
            AlterColumn("dbo.UserPreferences", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserPreferences", new[] { "Id", "Preference" });
            CreateIndex("dbo.AspNetUsers", new[] { "Preferences_Id", "Preferences_Preference" });
            AddForeignKey("dbo.AspNetUsers", new[] { "Preferences_Id", "Preferences_Preference" }, "dbo.UserPreferences", new[] { "Id", "Preference" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "Preferences_Id", "Preferences_Preference" }, "dbo.UserPreferences");
            DropIndex("dbo.AspNetUsers", new[] { "Preferences_Id", "Preferences_Preference" });
            DropPrimaryKey("dbo.UserPreferences");
            AlterColumn("dbo.UserPreferences", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.AspNetUsers", "Preferences_Preference");
            AddPrimaryKey("dbo.UserPreferences", "Id");
            CreateIndex("dbo.AspNetUsers", "Preferences_Id");
            AddForeignKey("dbo.AspNetUsers", "Preferences_Id", "dbo.UserPreferences", "Id");
        }
    }
}
