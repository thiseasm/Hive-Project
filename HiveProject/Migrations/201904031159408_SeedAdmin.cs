namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdmin : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserGender], [Thumbnail], [Age], [Radius], [Bio]) VALUES (N'7393a0f2-e4d0-45a5-b997-1f777ef66a1a', N'Admin@admin.com', 0, N'ADmN4w9v/r4Dk5Dzh8X9reCtqYMPxG/zltYRzULwtst2CZXmUeyCMRfV1PuzZs2xDQ==', N'5fbf57f3-e17e-4bde-a32a-5cf884f08559', NULL, 0, 0, NULL, 1, 0, N'Admin@admin.com', 1, N'bee.png', 90, 0, N'Hi,i''m new here!')

                   INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1174a03f-1f34-4878-a1e1-afdcff744b0d', N'Admin')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7393a0f2-e4d0-45a5-b997-1f777ef66a1a', N'1174a03f-1f34-4878-a1e1-afdcff744b0d')");
        }
        
        public override void Down()
        {
        }
    }
}
