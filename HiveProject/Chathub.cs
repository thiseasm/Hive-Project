using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HiveProject.Interfaces;
using HiveProject.Models;
using HiveProject.Models.ChatSystem;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace HiveProject
{
    public class ChatHub : Hub
    {
        private readonly Interfaces.IStore _store;
        private readonly UserManager<ApplicationUser> _manager;

        public ChatHub(IStore store, UserManager<ApplicationUser> manager)
        {
            _manager = manager;
            _store = store;
        }

        public async Task SendMessage(MessageViewModel model)
        {
            Message message = new Message
            {
                SenderId = model.SenderId,
                ReceiverId = model.ReceiverId,
                DateSent = model.Date.ToLocalTime(),
                Body = model.Body,
                Read = false
            };

            await _store.AddNewMessage();
            model.Date = message.DateSent;
            model.MessageId = message.MessageId;
            model.hasBeenRead = message.Read;

            await Clients.User(model.ReceiverId).SendAsync("ReceiveMessage", model);
        }
    }
}