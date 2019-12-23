using System.Linq;
using GhostChat.Data;
using GhostChat.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GhostChat.Controllers
{
    public class BaseController : Controller
    {
        private IRepository<User> users;

        protected User CurrentUser
        {
            get { return users.GetAll().Where(x => x.Username == HttpContext.Session.GetString("User")).SingleOrDefault(); }
        }

        public BaseController(IRepository<User> users)
        {
            this.users = users;
        }
    }
}