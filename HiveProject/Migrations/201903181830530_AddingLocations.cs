namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingLocations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentLocations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Radius = c.Int(nullable: false),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrentLocations", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.CurrentLocations", new[] { "Id" });
            DropTable("dbo.CurrentLocations");
        }
    }
}
