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
        public ActionResult SaveLocation(decimal lat, decimal lng)
        {
            var locations = Enumerable.Empty<Location>();
            var thisUserId = User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {

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
                }

                db.SaveChanges();
            }

            using (var db = new ApplicationDbContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                locations = db.Locations.Where(l => l.Id != thisUserId).ToList();
            }

            return Json(locations.Select(l => new Location()
            {
                Id = l.Id,
                Latitude = l.Latitude,
                Longitude = l.Longitude
            }).ToArray());
        }

        [HttpPost]
        public void GetLoggedUser(string UserId)
        {            
            ApplicationUser thisUser = new ApplicationUser();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                thisUser = db.Users.
                    Where(u => u.Id == UserId).SingleOrDefault();
            }

        }
    }
}