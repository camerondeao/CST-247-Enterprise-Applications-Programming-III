using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;
using Minesweeper_Web_Application.Services.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            return View("HomePageView", UserManagement.Instance._loggedUser);
        }

        [HttpPost]
        public ActionResult GetSelectedOption(FormCollection choice)
        {
            string result = choice["Selection"].ToString();
            Debug.WriteLine("Radio button value: " + result);
            switch (result)
            {
                case "Play":
                    return RedirectToAction("Index", "Game");
                case "HighScores":
                    return View("HighScores");
                case "Load":
                    return RedirectToAction("LoadGame", "Game");
                case "Profile":
                    return View("UserProfile", UserManagement.Instance._loggedUser);
                case "Logout":
                    Debug.WriteLine("Logged user: " + UserManagement.Instance._loggedUser.UserName);
                    UserManagement.Instance.LogOutUser();
                    if(UserManagement.Instance._loggedUser != null)
                    {
                        Debug.WriteLine("Logged user: " + UserManagement.Instance._loggedUser.UserName);
                    }
                    else
                    {
                        Debug.WriteLine("No user is logged into the application");
                    }
                    return RedirectToAction("Index", "Login");
                case "Delete":
                    GameDAO gameDAO = new GameDAO();
                    gameDAO.DeleteScore(UserManagement.Instance._loggedUser);
                    return View("HomePageView");
                default:
                    break;
            }
            return View("HomePageView");
        }

        private ActionResult ViewGameBoard()
        {
            return PartialView("/Views/Game/Game.cshtml");
        }
    }
}