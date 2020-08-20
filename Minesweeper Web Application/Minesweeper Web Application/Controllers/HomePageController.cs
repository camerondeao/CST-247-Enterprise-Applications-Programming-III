//Joey Cooper
//Version 1
//8-19-2020
//Created the Home Page Controller for the Minesweeper's Home Page and the GetSelectedOption method for the
//Radio Button selection in HomePageView.cshtml.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;

namespace Minesweeper_Web_Application.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult Index()
        {
            return View("HomePageView");
        }

        //GetSelectedOption
        //This will be used based on the selected radio button to route to the correct page
        //For now, it just routes to the Login page
        [HttpPost]
        public ActionResult GetSelectedOption()
        {
            return View("/Views/Login/Login.cshtml");
        }
    }
}