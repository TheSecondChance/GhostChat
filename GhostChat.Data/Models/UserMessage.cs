using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChat.Data.Models
{
    public class UserMessage
    {
        public Guid MessageId { get; set; }
        public Message Message { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}
