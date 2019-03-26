using HiveProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HiveProject.Managers
{
    public class InteractionManager
    {
        public List<Location> GetNearbyUsers(ApplicationUser User1)
        {
            List<Location> nearbyUsers = new List<Location>();
            var db = new ApplicationDbContext();

            nearbyUsers = db.Locations.Where(loc => loc.Id != User1.Id).ToList();
            return nearbyUsers;
        }

        //public IEnumerable<Location> GetNearbyUsers(ApplicationUser User1)
        //{
        //    IEnumerable<ApplicationUser> nearbyUsers = Enumerable.Empty<ApplicationUser>();

        //    using (var db = new ApplicationDbContext())
        //    {
        //        nearbyUsers = db.Users.
        //            Where(u => u.Id != User1.Id
        //            && User1.CurrentLocation.Distance(u.CurrentLocation) <= User1.Radius)
        //            .ToList();
        //    }
        //    return GetLocations(nearbyUsers);
        //}

        //public IEnumerable<Location> GetLocations (IEnumerable<ApplicationUser> nearbyUsers)
        //{
        //    IEnumerable<Location> nearbyLocations = Enumerable.Empty<Location>();

        //    var db = new ApplicationDbContext();
        //    nearbyLocations = db.Locations.
        //        Where(loc => nearbyUsers.All(u => u.Id == loc.Id))
        //        .ToList();

        //    return nearbyLocations;
        //}
    }
}