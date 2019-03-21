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
        public void SaveLocation(List<decimal> coords)
        {
            var thisUserId = User.Identity.GetUserId();

            Location location = new Location()
            {
                Latitude = coords[0],
                Longitude = coords[1],
                Id = thisUserId
            };

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var x = db.Locations.Add(location);
                db.SaveChanges();
            }
        }
    }
}