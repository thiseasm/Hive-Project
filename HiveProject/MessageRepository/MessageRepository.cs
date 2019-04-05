using HiveProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HiveProject.MessageRepositories
{
    public class MessageRepository
    {
          
        private ApplicationDbContext _context;
       

        public MessageRepository()
        {
            _context = new ApplicationDbContext();
        
        }

        public async Task AddNewMessage(Message msg)
        {

            {
                _context.Messages.Attach(msg);

                _context.Messages.Add(msg);
                _context.SaveChanges();
            }
        }
                   

        public async Task SetMessagesAsRead(string fromId, string toId)
        {
            var messages = _context.Messages.Where(m => m.SenderId == fromId && m.ReceiverId == toId);
            foreach (var msg in messages)
            {
                msg.Read = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessagesBetween(string userId, string receiverId)
        {
            var messages = _context.Messages
                .Include(m => m.Receiver)
                .Include(m => m.Sender)
                .Where(m => (m.SenderId == userId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == userId))
                .OrderBy(m => m.DateSent);
            return await messages.ToListAsync();
        }

       
    }
}


 