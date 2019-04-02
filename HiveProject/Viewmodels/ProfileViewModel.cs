using HiveProject.Models;
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
        public string username { get; set; }
        public string Thumbnail { get; set; }

        public ApplicationUser ActiveUser { get; set; }
        public ApplicationUser Receiver { get; set; }

        [StringLength(80,ErrorMessage ="Maximum 80 characters.")]
        public string Bio { get; set; }
    }
}