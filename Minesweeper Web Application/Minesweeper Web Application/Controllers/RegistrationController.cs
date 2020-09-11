using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace Minesweeper_Web_Application.Controllers
{
    public class RegistrationController : Controller
    {
        //Logger variables
        Logger logger = LogManager.GetLogger("fileLogger");
        readonly ArithmeticException ex;

        // GET: Registration
        public ActionResult Index()
        {
            //Log to logfile
            logger.Error(ex, "User on registration page");

            return View("Registration");
        }

        [HttpPost]
        public ActionResult RegisterAccount(UserModel model)
        {
            //Log to logfile
            logger.Error(ex, "User attempting to register new account");

            SecurityService service = new SecurityService();

            if(service.CheckUser(model))
            {
<<<<<<< HEAD
                //Log to logfile
                logger.Error(ex, "User cannot create new account; existing username");

=======
>>>>>>> 5fe7e8fabf7ea13a0952032f1716bcbee315e97d
                ModelState.AddModelError("Username", "This username is in use.");
                return View("Registration");
            }

            bool results = service.Register(model);

            if (results)
            {
                //Log to logfile
                logger.Error(ex, "User was able to create new account");

                //Clearing fields before entering Login.cshtml
                ModelState.Clear();
                return View("/Views/Login/Login.cshtml");
            }
            else
            {
                //Log to logfile
                logger.Error(ex, "User was unable to create new account");

                return View("Registration");
            }
        }

        public ActionResult Login()
        {
            //Log to logfile
            logger.Error(ex, "User returning to login page");

            return View("/Views/Login/Login.cshtml");
        }
    }
}