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
                ModelState.AddModelError("Username", "This username is in use.");
                return View("Registration");
            }

            bool results = service.Register(model);

            if (results)
            {
                //Clearing fields before entering Login.cshtml
                ModelState.Clear();
                return View("/Views/Login/Login.cshtml");
            }
            else
            {
                return View("Registration");
            }
        }

        public ActionResult Login()
        {
            return View("/Views/Login/Login.cshtml");
        }
    }
}