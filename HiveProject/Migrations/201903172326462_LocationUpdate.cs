namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "CurrentLocation_Latitude", "CurrentLocation_Longitude" }, "dbo.CurrentLocations");
            DropIndex("dbo.AspNetUsers", new[] { "CurrentLocation_Latitude", "CurrentLocation_Longitude" });
            RenameColumn(table: "dbo.CurrentLocations", name: "CurrentLocation_Latitude", newName: "CurrentLocationId");
            DropPrimaryKey("dbo.CurrentLocations");
            AddPrimaryKey("dbo.CurrentLocations", "CurrentLocationId");
            CreateIndex("dbo.CurrentLocations", "CurrentLocationId");
            AddForeignKey("dbo.CurrentLocations", "CurrentLocationId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "CurrentLocation_Latitude");
            DropColumn("dbo.AspNetUsers", "CurrentLocation_Longitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CurrentLocation_Longitude", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.AspNetUsers", "CurrentLocation_Latitude", c => c.Decimal(precision: 18, scale: 2));
            DropForeignKey("dbo.CurrentLocations", "CurrentLocationId", "dbo.AspNetUsers");
            DropIndex("dbo.CurrentLocations", new[] { "CurrentLocationId" });
            DropPrimaryKey("dbo.CurrentLocations");
            AddPrimaryKey("dbo.CurrentLocations", new[] { "Latitude", "Longitude" });
            RenameColumn(table: "dbo.CurrentLocations", name: "CurrentLocationId", newName: "CurrentLocation_Latitude");
            CreateIndex("dbo.AspNetUsers", new[] { "CurrentLocation_Latitude", "CurrentLocation_Longitude" });
            AddForeignKey("dbo.AspNetUsers", new[] { "CurrentLocation_Latitude", "CurrentLocation_Longitude" }, "dbo.CurrentLocations", new[] { "Latitude", "Longitude" });
        }
    }
}
