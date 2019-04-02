using HiveProject.MessageRepositories;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace HiveProject.Controllers
{
    public class MessagesController : ApiController
    {
        private MessageRepository _messageRepo;
        private string _currentLoggedUser { get; set; }

        public MessagesController()
        {
            _messageRepo = new MessageRepository();
            _currentLoggedUser = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }


        // POST: api/Messages
        public async Task<IHttpActionResult> Post([FromBody]string value)
        {
            var MessagesExchanged = await _messageRepo.GetMessagesBetween(_currentLoggedUser, value);
            return Ok(MessagesExchanged);
        }

    }
}