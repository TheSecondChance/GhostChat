using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostChat.BusinessLogic
{
    public class MessagesItem
    {
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public string Type { get; set; }
    }
}
