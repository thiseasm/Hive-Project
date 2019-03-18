using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiveProject.Models.ChatSystem
{
    public class Message
    {
        public int MessageId { get; set; }

        public string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }
        public virtual ApplicationUser Receiver { get; set; }

        public string Body { get; set; }
        public DateTime DateSent { get; set; }
        public bool Read { get; set; }

        public Message()
        {
            DateSent = DateTime.Now;
        }
    }
}
