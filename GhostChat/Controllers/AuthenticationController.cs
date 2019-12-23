using System.Linq;
using GhostChat.BusinessLogic;
using GhostChat.Data;
using GhostChat.Data.Models;
using GhostChat.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GhostChat.Controllers
{
    public class AuthenticationController : Controller
    {
        private IRepository<User> repository;
        public AuthenticationController(IRepository<User> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Title = "Register";
            return View();
        }

        public IActionResult Register(RegisterViewModel registerData)
        {
            if (ModelState.IsValid)
            {
                if (repository.GetAll().Where(x => x.Username == registerData.Username).SingleOrDefault() == null)
                {
                    if (repository.GetAll().Where(x => x.Email == registerData.Email).SingleOrDefault() == null)
                    {
                        User user = new User
                        {
                            Username = registerData.Username,
                            Email = registerData.Email,
                            Password = PasswordHashing.PasswordHash(registerData.Password)
                        };

                        repository.Add(user);
                        HttpContext.Session.SetString("User", user.Username);
                        return RedirectToAction("Site", "Main");
                    }
                }
            }

            return RedirectToAction("Register", "Authentication");
        }
    }
}