using System;
using System.Collections.Generic;

namespace GhostChat.Data.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }     
        public User Sender { get; set; }
        public virtual List<UserMessage> UserMessages { get; set; }

        public Message()
        {
            UserMessages = new List<UserMessage>();
        }
    }
}
