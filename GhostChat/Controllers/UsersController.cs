using System;
using System.Linq;
using GhostChat.Data;
using GhostChat.Data.Models;
using GhostChat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GhostChat.Controllers
{
    public class UsersController : BaseController 
    {
        private IRepository<User> users;
        private IRepository<Friendship> friendships;

        public UsersController(IRepository<User> users, IRepository<Friendship> friendships) : base(users)
        {
            this.users = users;
            this.friendships = friendships;
        }

        [HttpGet]
        public IActionResult List()
        {
            if (CurrentUser != null)
            {
                UsersViewModel usersViewModel = new UsersViewModel
                {
                    CurrentUser = this.CurrentUser,
                    OtherUsers = users.GetAll().Where(x => x.Id != CurrentUser.Id).ToList(),
                    UserFriends = friendships.GetAll().Where(x => x.RequestingUser.Id == CurrentUser.Id || x.AcceptingUser.Id == CurrentUser.Id).ToList()
                };
                ViewBag.Title = "Users";
                return View(usersViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult FriendRequest(Guid id)
        {
            if (CurrentUser != null)
            {
                User acceptingUser = users.GetById(id);
                friendships.Add(new Friendship
                {
                    RequestingUser = CurrentUser,
                    AcceptingUser = acceptingUser,
                    AreFriends = false
                });

                return RedirectToAction("List", "Users");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}