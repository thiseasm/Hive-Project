using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using HiveProject.MessageRepositories;
using HiveProject.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;

namespace HiveProject.ChatHub
{
    public class ChatHub : Hub
    {

        private static readonly ConcurrentDictionary<string, ChatUser> Users = new ConcurrentDictionary<string, ChatUser>();
        private MessageRepository _messageRepo;

        public string Url = "http://localhost:65081";

        public IWebProxy Proxy { get; set; }
        public HubConnection Connection { get; set; }


        public ChatHub()
        {
            _messageRepo = new MessageRepository();
            Connection = new HubConnection(Url);
        }

        public override Task OnConnected()
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new ChatUser
            {
                Name = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
            }

            return base.OnConnected();
        }

        public async Task SendMessage(MessageViewModel model)
        {

            Message message = new Message
            {
                SenderId = model.SenderId,
                ReceiverId = model.ReceiverId,
                DateSent = DateTime.Now,
                Body = model.Body,
                Read = false
            };

            await _messageRepo.AddNewMessage(message);
            model.Date = message.DateSent;
            model.MessageId = message.MessageId;
            model.hasBeenRead = message.Read;

            ChatUser sender = GetUser(Context.User.Identity.Name);
            if (Users.TryGetValue(model.ReceiverName, out ChatUser receiver))
            {
                IEnumerable<string> allReceivers;
                lock (receiver.ConnectionIds)
                {
                    allReceivers = receiver.ConnectionIds;
                }

                foreach (var cid in allReceivers)
                {
                    Clients.Client(cid).receiveMessage(model);
                }
            }
        }

        private ChatUser GetUser(string username)
        {
            Users.TryGetValue(username, out ChatUser user);

            return user;
        }
    }

    internal class ChatUser
    {
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}
    