using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiveProject.Models
{
    public class UserPreferences
    {        
        public List<ApplicationUser.Gender> Preferences { get; set; }
                
    }

    public class CurrentLocation
    {
        public int Radius { get; set; }

        public Location Location { get; set; }
    }

    public class Location
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}