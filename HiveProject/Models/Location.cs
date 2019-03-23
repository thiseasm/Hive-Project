using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiveProject.Models
{

    public class Location //todo rename to location
    {
        [ForeignKey("User")]
        public string Id { get; set; }

        public decimal Latitude { get; set; }
        
        public decimal Longitude { get; set; }

        public virtual ApplicationUser User { get; set; }

        //TODO Radius to user profile

        public double Distance(Location other)
        {
            var distanceLongitude = Longitude - other.Longitude;
            var distanceLatitude = Latitude - other.Latitude;
            var distance = Math.Sqrt((double)(distanceLatitude * distanceLatitude + distanceLongitude * distanceLongitude));
            return distance;
        }
    }

  
}