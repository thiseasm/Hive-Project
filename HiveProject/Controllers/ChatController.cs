
using HiveProject.Managers;
using HiveProject.MessageRepositories;
using HiveProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HiveProject.Controllers
{
    public class ChatController : Controller
    {
        
        
            private readonly MatchingManager matchRepo;
            private ApplicationDbContext _context;
            private MessageRepository _messageRepo;

            public ChatController()
            {
                _messageRepo = new MessageRepository();
                _context = new ApplicationDbContext();
                matchRepo = new MatchingManager();
            }

            [Authorize]
            public async Task<ActionResult> ChatAction()
            {
                ApplicationUser thisUser = _context.Users.Where(m => m.Id == m.Id).FirstOrDefault();
                ApplicationUser Receiver = thisUser;
                List<MessageViewModel> LiveChatMessages = new List<MessageViewModel>();


                return View(new MessageExchange
                {
                    ActiveUser = thisUser,
                    Receiver = thisUser,
                    LiveChatMessages = (await _messageRepo.GetMessagesBetween(thisUser.Id, Receiver.Id))
                    .Select(msg => new MessageViewModel
                    {
                        MessageId = msg.MessageId,
                        Body = msg.Body,
                        Date = msg.DateSent,
                        hasBeenRead = msg.Read,
                        SenderId = msg.SenderId,
                        SenderName = msg.Sender.UserName,
                        ReceiverId = msg.ReceiverId,
                        ReceiverName = msg.Receiver.UserName


                    }).ToList(),
                    UserMatches = (await matchRepo.ReturnMatchesAsync())
                });
            }

            [HttpPost]
            public async Task MarkConversationRead(string fromId, string toId)
            {
                await _messageRepo.SetMessagesAsRead(fromId, toId);
            }

        }
    }
