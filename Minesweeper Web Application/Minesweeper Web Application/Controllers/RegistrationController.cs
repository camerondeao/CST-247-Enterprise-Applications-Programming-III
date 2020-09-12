using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using Minesweeper_Web_Application.Services.Utility;
using System.Reflection;

namespace Minesweeper_Web_Application.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View("Registration");
        }

        [HttpPost]
        public ActionResult RegisterAccount(UserModel model)
        {
            SecurityService service = new SecurityService();

            if(service.CheckUser(model))
            {
                MineSweeperLogger.GetInstance().Warning(String.Format("User attempted account creation with {0} already registered.", model.UserName));
                ModelState.AddModelError("Username", "This username is in use.");
                return View("Registration");
            }

            bool results = service.Register(model);

            if (results)
            {
                MineSweeperLogger.GetInstance().Info(model.UserName + " created an account.");
                //Clearing fields before entering Login.cshtml
                ModelState.Clear();
                return View("/Views/Login/Login.cshtml");
            }
            else
            {
                MineSweeperLogger.GetInstance().Warning(model.UserName + " failed to create an account.");
                return View("Registration");
            }
        }

        public ActionResult Login()
        {
            return View("/Views/Login/Login.cshtml");
        }
    }
}