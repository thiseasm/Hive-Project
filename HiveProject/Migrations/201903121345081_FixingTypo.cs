namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingTypo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Thumbnail", c => c.String());
            DropColumn("dbo.AspNetUsers", "Thumnail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Thumnail", c => c.String());
            DropColumn("dbo.AspNetUsers", "Thumbnail");
        }
    }
}
