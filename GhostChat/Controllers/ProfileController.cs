using System;
using System.Linq;
using GhostChat.BusinessLogic;
using GhostChat.Data;
using GhostChat.Data.Models;
using GhostChat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GhostChat.Controllers
{
    public class ProfileController : BaseController
    {
        private IRepository<User> users;
        private IRepository<Friendship> friendships;
        public ProfileController(IRepository<User> users, IRepository<Friendship> friendships) : base(users)
        {
            this.users = users;
            this.friendships = friendships;
        }

        [HttpGet]
        public IActionResult Page()
        {
            if (CurrentUser != null)
            {
                FriendshipManager manager = new FriendshipManager(friendships);

                ProfileViewModel profileViewModel = new ProfileViewModel
                {
                    FriendRequests = friendships.GetAll().Where(x => x.AcceptingUser.Id == CurrentUser.Id && !x.AreFriends).ToList(),
                    Friends = manager.GetFriendsList(CurrentUser)
                };

                ViewBag.Title = "Profile";
                return View(profileViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AcceptFriendship(Guid id)
        {
            if (CurrentUser != null)
            {
                Friendship accept = friendships.GetAll().Where(x => x.RequestingUser.Id == id && x.AcceptingUser.Id == CurrentUser.Id).Single();
                accept.AreFriends = true;
                friendships.Update(accept);

                return RedirectToAction("Page", "Profile");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RejectFriendship(Guid id)
        {
            if (CurrentUser != null)
            {
                Friendship reject = friendships.GetAll().Where(x => x.RequestingUser.Id == id && x.AcceptingUser.Id == CurrentUser.Id).Single();
                friendships.Remove(reject);
                return RedirectToAction("Page", "Profile");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RemoveFriend(Guid id)
        {
            if (CurrentUser != null)
            {
                FriendshipManager manager = new FriendshipManager(friendships);

                User friend = users.GetAll().Where(x => x.Id == id).Single();
                manager.RemoveFriendship(friend, CurrentUser);

                return RedirectToAction("Page", "Profile");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}