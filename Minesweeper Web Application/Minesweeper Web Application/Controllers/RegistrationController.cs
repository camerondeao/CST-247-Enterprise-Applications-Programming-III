using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minesweeper_Web_Application.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public string Index()
        {
            //return View();
            return @"<b>Just a test from reg Index THIS IS AN ADDITIONAL TEST</b>";
        }

        [HttpPost]
        public ActionResult RegisterAccount(UserModel model)
        {
            SecurityService service = new SecurityService();
            //Pass the UserModel in the service.Register() call.
            bool results = service.Register(model);
            if (results)
            {
                return View("Login");
            }
            else
            {
                //Change this return view if necessary.
                //This fires in the event a user can't be registered to the database.
                return View("Registration");
            }
        }
    }
}