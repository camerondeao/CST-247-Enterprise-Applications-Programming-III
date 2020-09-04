using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minesweeper_Web_Application.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            SecurityService service = new SecurityService();
            bool results = service.Authenticate(model);

            if (results)
            {
                //UserManagement.Instance._loggedUser = model;
                Debug.WriteLine("Logged in User: " + UserManagement.Instance._loggedUser.UserName);
                Debug.WriteLine("Login passed!");
                //return View("LoginPassed");
                return RedirectToAction("MainPage", "Home");
            }
            else
            {
                Debug.WriteLine("Login failed!");
                return View("LoginFailed");
            }
        }
    }
}