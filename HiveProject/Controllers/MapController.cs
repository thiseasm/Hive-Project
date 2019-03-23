using HiveProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace HiveProject.Controllers
{
    public class MapController : Controller
    {
        [HttpPost]
        public void SaveLocation(decimal lat, decimal lng)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            var thisUserId = User.Identity.GetUserId();

            Location location = new Location()
            {
                Latitude = lat,
                Longitude = lng,
                Id = thisUserId
            };

            bool inDatabase = db.Locations.Any(u => u.Id == thisUserId);
            if (inDatabase == false)
            {
                var saveLocationQuery = db.Locations.Add(location);
                db.SaveChanges();
            }
            else
            {
                var getLocation = from loc in db.Locations
                                  where loc.Id == thisUserId
                                  select loc;
                foreach (Location position in getLocation)
                {
                    position.Latitude = location.Latitude;
                    position.Longitude = location.Longitude;
                }

                db.SaveChanges();
            }
            
            
        }
    }
}