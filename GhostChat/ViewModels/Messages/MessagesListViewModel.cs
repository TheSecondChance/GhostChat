using GhostChat.BusinessLogic;
using GhostChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostChat.ViewModels.Messages
{
    public class MessagesListViewModel
    {
        public List<User> Friends { get; set; }
        public List<MessagesItem> Messages { get; set; }
    }
}
