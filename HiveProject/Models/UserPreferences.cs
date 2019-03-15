using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiveProject.Models
{
    public class UserPreferences
    {        
        public List<ApplicationUser.Gender> Preferences { get; set; }
                
    }

    public class CurrentLocation
    {
        public int Radius { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal Latitude { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal Longitude { get; set; }
    }

  
}