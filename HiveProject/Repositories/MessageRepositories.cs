using HiveProject.Interfaces;
using HiveProject.Models;
using HiveProject.Models.ChatSystem;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HiveProject.Repositories
{
    public class MessageRepositories
    {

        public class MessageRepository : IStore

        {
            private ApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _manager;
            private readonly IStore _messageRepo;

            public MessageRepository(ApplicationDbContext context, UserManager<ApplicationUser> manager)
            {
                _context = context;
                _manager = manager;


            }

            public async Task AddNewMessage( )
            {
                await _context.Messages.AnyAsync();
                await _context.SaveChangesAsync();
            }


            public async Task<List<Message>> GetMessagesBetween(string userId, string receiverId)
            {
                var messages = _context.Messages.Include(m => m.Receiver).Include(m => m.Sender).Where(m => (m.SenderId == userId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == userId)).OrderBy(m => m.DateSent);
                return await messages.ToListAsync();
            }

            public async Task<List<Message>> GetUnreadMessages(string myId)
            {
                var messages = _context.Messages.Where(m => m.ReceiverId == myId && !m.Read).Include(m => m.Sender).Include(m => m.Receiver).OrderBy(m => m.DateSent);

                return await messages.ToListAsync();
            }


            public async Task SetMessageisRead(string fromId, string toId)
            {
                var messages = _context.Messages.Where(m => m.SenderId == fromId && m.ReceiverId == toId);
                foreach (var messaz in messages)
                {
                    messaz.Read = true;
                }
                await _context.SaveChangesAsync();
            }

            public async Task DeleteMessagesFromOrBy(string id)
            {
                var messages = _context.Messages.Where(m => m.SenderId == id || m.ReceiverId == id);
                _context.Messages.RemoveRange(messages);
                await _context.SaveChangesAsync();
            }


        }
    }
}