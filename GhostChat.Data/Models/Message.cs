using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChat.Data.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }

        public Guid SenderId { get; set; }
        public User Sender { get; set; }

        public Guid RecipientId { get; set; }
        public MessageRecipient Recipient { get; set; }
    }
}
