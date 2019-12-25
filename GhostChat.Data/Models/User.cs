using System;
using System.Collections.Generic;

namespace GhostChat.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual List<UserMessage> UserMessages { get; set; }

        public User()
        {
            UserMessages = new List<UserMessage>();
        }
    }
}
