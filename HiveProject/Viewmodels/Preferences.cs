using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HiveProject.Viewmodels
{
    public class Preferences
    {
        [Display(Name = "Interested In")]
        [Range(1, 3, ErrorMessage = "The Interested In field is required.")]
        public Models.ApplicationUser.Gender Preference { get; set; }

        [Range(5,30)]
        [Display(Name ="Search Radius")]
        public int Range { get; set; }
    }
}