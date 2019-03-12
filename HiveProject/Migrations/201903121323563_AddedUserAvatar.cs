namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserAvatar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Thumnail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Thumnail");
        }
    }
}
