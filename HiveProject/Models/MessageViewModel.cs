using HiveProject.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiveProject.Models
{
    public class MessageViewModel
    {

        public int MessageId { get; set; }

        public string SenderId { get; set; }
        public string SenderName { get; set; }

        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }


        public ApplicationUser ActiveUser { get; set; }
        public ApplicationUser Receiver { get; set; }

        public string Body { get; set; }
        public DateTime Date { get; set; }

        public UsersViewModel UserDetail { get; set; }

        public bool hasBeenRead { get; set; }
        public bool IsOnline { get; set; }
        public string LastSeen { get; set; }
        public string Thumbnail { get; set; }
    }

    public class MessageExchange
    {
        public ApplicationUser ActiveUser { get; set; }
        public ApplicationUser Receiver { get; set; }

        public List<MessageViewModel> LiveChatMessages { get; set; }
        public List<UsersViewModel> UserMatches { get; set; }

        public string ProfilePicture { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}