using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostChat.BusinessLogic;
using GhostChat.Data;
using GhostChat.Data.Models;
using GhostChat.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GhostChat.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<User> repository;
        public HomeController(IRepository<User> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginVM loginData)
        {
            if (ModelState.IsValid)
            {
                if (repository.GetAll().Where(x => x.Username == loginData.Username).SingleOrDefault() != null)
                {
                    string hashToVerify = repository.GetAll()
                        .Where(x => x.Username == loginData.Username)
                        .SingleOrDefault().Password;
                    if (PasswordHashing.PasswordVerify(loginData.Password, hashToVerify))
                    {
                        HttpContext.Session.SetString("User", loginData.Username);
                        return RedirectToAction("Site", "Home");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Site()
        {
            return View();
        }
    }
}