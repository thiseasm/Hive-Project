namespace HiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetUsersFiltered_SP : DbMigration
    {
        public override void Up()
        {

            CreateStoredProcedure("dbo.GetUsersFiltered",
              x => new
              {
                  Id = x.String(maxLength: 128),
                  Lat = x.Decimal(10, 7),
                  Long = x.Decimal(10, 7),
                  Range = x.Int(),
                  Preference=x.Int()
              },
              body: @"declare @geo1 geography = geography::Point(@LAT, @LONG, 4326)

    SELECT Locations.Id as Id,Locations.Latitude as Latitude,Locations.Longitude as Longitude FROM Locations join AspNetUsers on AspNetUsers.Id=Locations.Id
    WHERE (@geo1.STDistance(geography::Point([Latitude],
    [Longitude], 4326)))/1000 < @Range and not exists (select*from Likes where SenderId=@Id and ReceiverId=Locations.Id) and @Id!=Locations.Id and AspNetUsers.UserGender=@Preference");
        }
        
        public override void Down()
        {
        }
    }
}
