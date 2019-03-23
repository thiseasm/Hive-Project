namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MapIntegration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CurrentLocations", "CurrentLocationId", "dbo.AspNetUsers");
            DropIndex("dbo.CurrentLocations", new[] { "CurrentLocationId" });
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Radius", c => c.Int(nullable: false));
            DropTable("dbo.CurrentLocations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CurrentLocations",
                c => new
                    {
                        CurrentLocationId = c.String(nullable: false, maxLength: 128),
                        Radius = c.Int(nullable: false),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CurrentLocationId);
            
            DropForeignKey("dbo.Locations", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Locations", new[] { "Id" });
            DropColumn("dbo.AspNetUsers", "Radius");
            DropTable("dbo.Locations");
            CreateIndex("dbo.CurrentLocations", "CurrentLocationId");
            AddForeignKey("dbo.CurrentLocations", "CurrentLocationId", "dbo.AspNetUsers", "Id");
        }
    }
}
