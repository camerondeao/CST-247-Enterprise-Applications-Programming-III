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
using NLog;
using Minesweeper_Web_Application.Services.Utility;

namespace Minesweeper_Web_Application.Controllers
{
    [HandleError]
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
            //List<object> testing = new List<object>();
            //testing.Add(1);
            //testing.Add(1.2);
            //testing.Add(UserManagement.Instance._loggedUser);
            return View("HomePageView", UserManagement.Instance._loggedUser);
        }

        public ActionResult MainNoUser()
        {
            
            return View("HomeNoUser");
        }
        
        [HttpPost]
        public ActionResult GetSelectedOption(FormCollection choice)
        {
            GameDAO gameDAO = new GameDAO();
            string result = choice["Selection"].ToString();
            Debug.WriteLine("Radio button value: " + result);
            switch (result)
            {
                case "Play":
                    if (!CheckUserLogin())
                    {
                        throw new Exception("Exception occurred.");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Game");
                    }
                    
                case "Load":
                    
                    if (!gameDAO.CheckIfScoreExists(UserManagement.Instance._loggedUser))
                    {
                        ModelState.AddModelError(string.Empty, "No save exists!");
                        return View("HomePageView", UserManagement.Instance._loggedUser);
                    }
                    else
                    {
                        return RedirectToAction("LoadGame", "Game");
                    }

                case "HighScores":
                    List<LeaderBoard> dataScores = new List<LeaderBoard>();
                    LeaderBoardService service = new LeaderBoardService();
                    dataScores = service.GetScores(dataScores);
                    return View("HighScores", dataScores);

                case "Profile":
                    if (!CheckUserLogin())
                    {
                        throw new Exception("Exception occurred.");
                    }
                    else
                    {
                        return View("UserProfile", UserManagement.Instance._loggedUser);
                    }
                    
                case "Logout":
                    Debug.WriteLine("Logged user: " + UserManagement.Instance._loggedUser.UserName);
                    MineSweeperLogger.GetInstance().Info(String.Format("{0} has logged out of the application", UserManagement.Instance._loggedUser.UserName));
                    UserManagement.Instance.LogOutUser(); 
                    return RedirectToAction("Index", "Login");

                case "Delete":
                    gameDAO.DeleteScore(UserManagement.Instance._loggedUser);
                    return View("HomePageView", UserManagement.Instance._loggedUser);

                default:
                    break;
                    
            }
            return View("HomePageView");
        }

        private bool CheckUserLogin()
        {
            if (UserManagement.Instance._loggedUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}