using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minesweeper_Web_Application.Controllers
{
    public class LoginController : Controller
    {
        public string testing = "THIS IS A TEST FOR THE VIDEO!";
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("Login");
            }

            SecurityService service = new SecurityService();
            bool results = service.Authenticate(model);

            if (results)
            {
                return View("LoginPassed");
            }
            else
            {
                return View("LoginFailed");
            }
        }
    }
}