
using HiveProject.Managers;
using HiveProject.MessageRepositories;
using HiveProject.Models;
using HiveProject.Viewmodels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            ApplicationUser thisUser = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();
            ApplicationUser Receiver = new ApplicationUser();
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


       
    }
}