using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChat.Data.Models
{
    public class Friendship
    {
        public Guid Id { get; set; }
        public User RequestingUser { get; set; }
        public User AcceptingUser { get; set; }
        public bool AreFriends { get; set; }
    }
}
