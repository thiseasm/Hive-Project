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
                var distanceLongitude = user.Location.Longitude - User1.Location.Longitude;
                var distanceLatitude = user.Location.Latitude - User1.Location.Latitude;
                var distance = Math.Sqrt((double)(distanceLatitude * distanceLatitude + distanceLongitude * distanceLongitude));

                if (distance <= User1.Location.Radius)
                {
                    nearbyUsers.Add(user);
                }
            }

            return nearbyUsers;
        }
    }
}