namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingPrecision_AddingBio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Bio", c => c.String());
            AlterColumn("dbo.Locations", "Latitude", c => c.Decimal(nullable: false, precision: 10, scale: 7));
            AlterColumn("dbo.Locations", "Longitude", c => c.Decimal(nullable: false, precision: 10, scale: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Locations", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AspNetUsers", "Bio");
        }
    }
}
