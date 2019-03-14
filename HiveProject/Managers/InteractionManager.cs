using HiveProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiveProject.Managers
{
    public class InteractionManager
    {
        public List<ApplicationUser> GetNearbyUsers(ApplicationUser User1)
        {
            List<ApplicationUser> allUsers = new List<ApplicationUser>();
            List<ApplicationUser> nearbyUsers = new List<ApplicationUser>();

            using (var db = new ApplicationDbContext())
            {
                allUsers = db.Users.ToList();
            }

            foreach (var user in allUsers)
            {
                var distanceLongitude = user.CurrentLocation.Location.Longitude - User1.CurrentLocation.Location.Longitude;
                var distanceLatitude = user.CurrentLocation.Location.Latitude - User1.CurrentLocation.Location.Latitude;
                var distance = Math.Sqrt((double)(distanceLatitude * distanceLatitude + distanceLongitude * distanceLongitude));

                if (distance <= User1.CurrentLocation.Radius)
                {
                    nearbyUsers.Add(user);
                }
            }

            return nearbyUsers;
        }
    }
}