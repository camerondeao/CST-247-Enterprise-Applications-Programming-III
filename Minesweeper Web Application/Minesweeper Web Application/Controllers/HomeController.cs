using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Minesweeper_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("/Views/Login/Login.cshtml");
        }

        //GET: MainPage
        public ActionResult MainPage()
        {
            return View("/Views/Home/HomePageView.cshtml");
        }

        [HttpPost]
        public ActionResult GetSelectedOption(FormCollection choice)
        {
            string result = choice["Selection"].ToString();
            Debug.WriteLine("Radio button value: " + result);
            if (result == "Play")
            {
                //return View("/Views/Game/Game.cshtml");
                return RedirectToAction("Index", "Game");
            }
            else
            {
                return View("/Views/Login/Login.cshtml");
            }
            
            //return View("/Views/Login/Login.cshtml");
        }

        private ActionResult ViewGameBoard()
        {
            return View("/Views/Game/Game.cshtml");
        }
    }
}