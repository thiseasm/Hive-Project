using HiveProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;


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
                var distanceLongitude = user.CurrentLocation.Longitude - User1.CurrentLocation.Longitude;
                var distanceLatitude = user.CurrentLocation.Latitude - User1.CurrentLocation.Latitude;
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