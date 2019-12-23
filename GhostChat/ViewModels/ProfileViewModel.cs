using GhostChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostChat.ViewModels
{
    public class ProfileViewModel
    {
        public List<Friendship> FriendRequests { get; set; }
        public List<User> Friends { get; set; }
    }
}
