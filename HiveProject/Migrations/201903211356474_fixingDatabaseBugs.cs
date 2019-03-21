namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingDatabaseBugs : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.MyUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.MatchedUserId)
                .Index(t => t.MyUserId)
                .Index(t => t.MatchedUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "MatchedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Matches", "MyUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Matches", new[] { "MatchedUserId" });
            DropIndex("dbo.Matches", new[] { "MyUserId" });
            DropTable("dbo.Matches");
        }
    }
}
