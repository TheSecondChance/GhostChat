using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GhostChat.Controllers
{
    public class MainController : Controller
    {
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
    }
}