using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiveProject.Models
{
    public class CurrentLocation
    {
        [ForeignKey("User")]
        public string CurrentLocationId { get; set; }

        public int Radius { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

  
}