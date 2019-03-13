namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplaceBirthdateWithAge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Birthday");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "Age");
        }
    }
}
