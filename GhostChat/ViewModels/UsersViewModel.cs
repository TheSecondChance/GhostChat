using GhostChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostChat.ViewModels
{
    public class UsersViewModel
    {
        public User CurrentUser { get; set; }
        public List<User> OtherUsers { get; set; }
        public List<Friendship> UserFriends { get; set; }
    }
}
