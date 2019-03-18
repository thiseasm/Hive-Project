using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiveProject.Models.ChatSystem
{
    public class MessageViewModel
    {
        public int MessageId { get; set; }

        public string SenderId { get; set; }
        public string SenderName { get; set; }

        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public string Body { get; set; }
        public DateTime Date { get; set; }
        public bool hasBeenRead { get; set; }
    }
}