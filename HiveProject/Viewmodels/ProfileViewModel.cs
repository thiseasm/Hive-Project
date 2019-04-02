using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HiveProject.Viewmodels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Thumbnail { get; set; }

        [StringLength(80,ErrorMessage ="Maximum 80 characters.")]
        public string Bio { get; set; }
    }
}