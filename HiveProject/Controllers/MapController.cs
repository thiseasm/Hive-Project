using HiveProject.Models;
using System;
using HiveProject.Viewmodels;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using HiveProject.Managers;

namespace HiveProject.Controllers
{
    public class MapController : Controller
    {
        [HttpPost]
        public ActionResult SaveLocation(string lat, string lng)
        {
            var latitude = decimal.Parse(lat ?? "0", System.Globalization.CultureInfo.InvariantCulture);
            var longitude = decimal.Parse(lng ?? "0", System.Globalization.CultureInfo.InvariantCulture);
            var manager = new MatchingManager();
            var locations = Enumerable.Empty<UsersInRadius>();
            var thisUserId = User.Identity.GetUserId();

            using (var db = new ApplicationDbContext())
            {
                Location location = new Location()
                {
                    Latitude = latitude,
                    Longitude = longitude,
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

            locations = manager.GetUsersAsync(latitude, longitude);

            return Json(locations);
        }

        [HttpPost]
        public ActionResult ClickedUser(string UserId)
        {
            ApplicationUser thisUser = new ApplicationUser();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                thisUser = db.Users.
                    Where(u => u.Id == UserId).SingleOrDefault();
            }
            UsersViewModel clicked = new UsersViewModel()
            {
                Id = thisUser.Id,
                Age = thisUser.Age,
                Gender = thisUser.UserGender,
                Thumbnail = thisUser.Thumbnail,
                Username = thisUser.UserName,
                Bio = thisUser.Bio
            };

            return PartialView(clicked);
        }
    }
}