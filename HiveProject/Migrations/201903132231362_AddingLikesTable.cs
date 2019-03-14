namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingLikesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.String(maxLength: 128),
                        ReceiverId = c.String(maxLength: 128),
                        Like = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ReceiverId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "ReceiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.Likes", new[] { "ReceiverId" });
            DropIndex("dbo.Likes", new[] { "SenderId" });
            DropTable("dbo.Likes");
        }
    }
}
