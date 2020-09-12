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
        //Logger variables
        //Logger logger = LogManager.GetLogger("fileLogger");
        //readonly ArithmeticException ex;

        // GET: Registration
        public ActionResult Index()
        {
            return View("Registration");
        }

        [HttpPost]
        public ActionResult RegisterAccount(UserModel model)
        {
            //Log to logfile
            //logger.Error(ex, "User attempting to register new account");

            SecurityService service = new SecurityService();

            if(service.CheckUser(model))
            {
                MineSweeperLogger.GetInstance().Warning("User attempted account creation with " + model.UserName + " already registered.");
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
                //Log to logfile
                //logger.Error(ex, "User was unable to create new account");
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