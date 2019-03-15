using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HiveProject.Models
{
    public class Matches
    {
        public int Id { get; set; }

        public string MyUserId { get; set; }

        public string MatchedUserId { get; set; }

        public bool SeenByMyUser{ get; set; }

        [ForeignKey("MyUserId")]
        public virtual ApplicationUser MatchedUser1 { get; set; }

        [ForeignKey("MatchedUserId")]
        public virtual ApplicationUser MatchedUser2 { get; set; }

    }
}