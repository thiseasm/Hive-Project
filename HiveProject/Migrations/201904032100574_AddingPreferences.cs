namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPreferences : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Preferences", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Preferences");
        }
    }
}
