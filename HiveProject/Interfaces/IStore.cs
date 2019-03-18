using HiveProject.Models.ChatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiveProject.Interfaces
{
   public interface IStore
    {
        Task AddNewMessage( );
        Task SetMessageisRead(string fromId, string toId);

        Task<List<Message>> GetMessagesBetween(string id1, string id2);
    }
}
