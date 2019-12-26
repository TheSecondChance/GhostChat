using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GhostChat.Data;
using GhostChat.Data.Models;
using GhostChat.BusinessLogic;
using System.Collections.Generic;
using System;
using GhostChat.ViewModels;
using GhostChat.ViewModels.Messages;

namespace GhostChat.Controllers
{
    public class MainController : BaseController
    {
        private IRepository<User> users;
        private IRepository<Friendship> friendships;
        private ApplicationContext dbContext;

        public MainController(IRepository<User> users, IRepository<Friendship> friendships, ApplicationContext context) : base(users)
        {          
            this.users = users;
            this.friendships = friendships;
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Site()
        {
            if (CurrentUser != null)
            {
                FriendshipManager manager = new FriendshipManager(friendships);

                List<User> userFriends = manager.GetFriendsList(CurrentUser);
                ViewBag.Username = CurrentUser.Username;
                ViewBag.Title = "GhostChat";
                return View(userFriends);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Messages(string id)
        {
            if (CurrentUser != null)
            {
                User friend = users.GetAll().Find(x => x.Username == id);
                MessageManager messageManager = new MessageManager(dbContext);
                List<MessagesItem> messages = messageManager.GetMessages(CurrentUser, friend);

                FriendshipManager friendManager = new FriendshipManager(friendships);
                List<User> friends = friendManager.GetFriendsList(CurrentUser);

                MessagesListViewModel viewModel = new MessagesListViewModel
                {
                    Friends = friends,
                    Messages = messages
                };

                ViewBag.Title = "GhostChat";
                ViewBag.RecipientId = friend.Id;
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult SendMessage(MessageViewModel messageViewModel)
        {
            if (CurrentUser != null)
            {
                User recipient = users.GetById(messageViewModel.RecipientId);
                if (ModelState.IsValid)
                {
                    Message message = new Message
                    {
                        Text = Ghost.Encrypt(messageViewModel.Text, Ghost.EncryptionKey),
                        CreationTime = DateTime.Now,
                        Sender = CurrentUser,
                    };

                    dbContext.Messages.Add(message);
                    recipient.UserMessages.Add(new UserMessage { UserID = recipient.Id, MessageId = message.Id });
                    dbContext.SaveChanges();

                    return Redirect($"/Main/Messages/{recipient.Username}");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}