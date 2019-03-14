using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiveProject.Viewmodels
{
    public class MatchingViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Thumbnail { get; set; }

        public int Age { get; set; }

        public Models.ApplicationUser.Gender Gender { get; set; }

    }
}