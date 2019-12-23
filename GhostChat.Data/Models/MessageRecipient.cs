using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChat.Data.Models
{
    public class MessageRecipient
    {
        public Guid Id { get; set; }
        public virtual List<Message> Messages { get; set; }
        public User Recipient { get; set; }
    }
}
