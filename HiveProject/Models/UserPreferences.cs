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
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 2)]
        public ApplicationUser.Gender Preference { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

    }

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