namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingPreferencesAndFixingLocationTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matches", "MyUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Matches", "MatchedUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Matches", new[] { "MyUserId" });
            DropIndex("dbo.Matches", new[] { "MatchedUserId" });
            CreateTable(
                "dbo.CurrentLocations",
                c => new
                    {
                        CurrentLocationId = c.String(nullable: false, maxLength: 128),
                        Radius = c.Int(nullable: false),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CurrentLocationId)
                .ForeignKey("dbo.AspNetUsers", t => t.CurrentLocationId)
                .Index(t => t.CurrentLocationId);
            
           //DropTable("dbo.Matches");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MyUserId = c.String(maxLength: 128),
                        MatchedUserId = c.String(maxLength: 128),
                        SeenByMyUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.CurrentLocations", "CurrentLocationId", "dbo.AspNetUsers");
            DropIndex("dbo.CurrentLocations", new[] { "CurrentLocationId" });
            DropTable("dbo.CurrentLocations");
            CreateIndex("dbo.Matches", "MatchedUserId");
            CreateIndex("dbo.Matches", "MyUserId");
            AddForeignKey("dbo.Matches", "MatchedUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Matches", "MyUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
