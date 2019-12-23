using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GhostChat.Data;
using GhostChat.Data.Models;
using GhostChat.BusinessLogic;
using System.Collections.Generic;

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
                FriendshipManager manager = new FriendshipManager(friendships);

                List<User> friends = manager.GetFriendsList(CurrentUser);
                ViewBag.Username = CurrentUser.Username;
                ViewBag.Title = "GhostChat";
                return View(friends);
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