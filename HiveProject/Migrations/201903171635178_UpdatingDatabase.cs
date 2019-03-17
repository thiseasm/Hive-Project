namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Email : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentLocations",
                c => new
                    {
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Radius = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Latitude, t.Longitude });
            
            AddColumn("dbo.AspNetUsers", "CurrentLocation_Latitude", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.AspNetUsers", "CurrentLocation_Longitude", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.AspNetUsers", new[] { "CurrentLocation_Latitude", "CurrentLocation_Longitude" });
            AddForeignKey("dbo.AspNetUsers", new[] { "CurrentLocation_Latitude", "CurrentLocation_Longitude" }, "dbo.CurrentLocations", new[] { "Latitude", "Longitude" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "CurrentLocation_Latitude", "CurrentLocation_Longitude" }, "dbo.CurrentLocations");
            DropIndex("dbo.AspNetUsers", new[] { "CurrentLocation_Latitude", "CurrentLocation_Longitude" });
            DropColumn("dbo.AspNetUsers", "CurrentLocation_Longitude");
            DropColumn("dbo.AspNetUsers", "CurrentLocation_Latitude");
            DropTable("dbo.CurrentLocations");
        }
    }
}
