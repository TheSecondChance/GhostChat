using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GhostChat.Data;
using GhostChat.Data.Models;

namespace GhostChat.Controllers
{
    public class MainController : BaseController
    {
        private IRepository<User> users;
        private IRepository<Friendship> friendships;
        public MainController(IRepository<User> users, IRepository<Friendship> friendships) : base(users)
        {          
            this.users = users;
            this.friendships = friendships;
        }

        [HttpGet]
        public IActionResult Site()
        {
            if (CurrentUser != null)
            {
                ViewBag.username = CurrentUser.Username;
                ViewBag.Title = "GhostChat";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}