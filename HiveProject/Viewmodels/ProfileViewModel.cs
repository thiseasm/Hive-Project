using HiveProject.Models;
using System;
using System.Collections.Generic;
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
    }
}