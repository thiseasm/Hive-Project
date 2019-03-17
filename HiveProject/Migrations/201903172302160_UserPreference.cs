namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPreference : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPreferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Preference = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Preferences_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Preferences_Id");
            AddForeignKey("dbo.AspNetUsers", "Preferences_Id", "dbo.UserPreferences", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Preferences_Id", "dbo.UserPreferences");
            DropIndex("dbo.AspNetUsers", new[] { "Preferences_Id" });
            DropColumn("dbo.AspNetUsers", "Preferences_Id");
            DropTable("dbo.UserPreferences");
        }
    }
}
