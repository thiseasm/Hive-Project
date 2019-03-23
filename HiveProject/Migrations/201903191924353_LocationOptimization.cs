namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationOptimization : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CurrentLocations", newName: "Locations");
            AddColumn("dbo.AspNetUsers", "Radius", c => c.Int(nullable: false));
            DropColumn("dbo.Locations", "Radius");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "Radius", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Radius");
            RenameTable(name: "dbo.Locations", newName: "CurrentLocations");
        }
    }
}
