using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostChat.Data;
using GhostChat.Data.Models;

namespace GhostChat.ViewModels
{
    public class UsersVM
    {
        public List<User> users;
        private IRepository<User> repository;
        public UsersVM(IRepository<User> repository)
        {
            this.repository = repository;
            users = new List<User>();

            
        }
    }
}
