using System.Linq;
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
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginData)
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
                        return RedirectToAction("Site", "Main");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}