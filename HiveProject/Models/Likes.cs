using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HiveProject.Models
{
    public class Likes
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public bool? Like { get; set; }

        [ForeignKey("SenderId")]
        public virtual ApplicationUser User1 { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual ApplicationUser User2 { get; set; }
    }
}