using HiveProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiveProject.Viewmodels
{
    public class UsersViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Thumbnail { get; set; }

        public int Age { get; set; }


        public ApplicationUser ActiveUser { get; set; }
        public ApplicationUser Receiver { get; set; }

        public Models.ApplicationUser.Gender Gender { get; set; }

        public string About  {get; set;}
        
        public string Location { get; set; }

    }
}