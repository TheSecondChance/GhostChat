using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GhostChat.Data;
using GhostChat.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace GhostChat.Controllers
{
    public class MainController : Controller
    {
        private IRepository<User> repository;
        public MainController(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public IActionResult Site()
        {
            string username = HttpContext.Session.GetString("User");

            if (username != null)
            {
                ViewBag.username = username;
                ViewBag.Title = "GhostChat";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Users()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                string currentUser = HttpContext.Session.GetString("User");
                List<User> users = repository.GetAll().Where(x => x.Username != currentUser).ToList();             
                return View(users);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}