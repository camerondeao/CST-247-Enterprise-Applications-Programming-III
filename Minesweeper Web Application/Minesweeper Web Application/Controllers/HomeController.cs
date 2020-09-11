using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Business;
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

namespace Minesweeper_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        //Logger variables
        Logger logger = LogManager.GetLogger("fileLogger");
        readonly ArithmeticException ex;

        // GET: Home
        public ActionResult Index()
        {
            //Log to logfile
            logger.Error(ex, "User on home page");

            return View("/Views/Login/Login.cshtml");
        }

        //GET: MainPage
        public ActionResult MainPage()
        {
            //Log to logfile
            logger.Error(ex, "User going to main page");

            return View("HomePageView", UserManagement.Instance._loggedUser);
        }

        [HttpPost]
        public ActionResult GetSelectedOption(FormCollection choice)
        {
<<<<<<< HEAD
            //Log to logfile
            logger.Error(ex, "User selected an option");
=======
            
>>>>>>> 5fe7e8fabf7ea13a0952032f1716bcbee315e97d

            string result = choice["Selection"].ToString();
            Debug.WriteLine("Radio button value: " + result);
            switch (result)
            {
                case "Play":
<<<<<<< HEAD
                    {
                        //Log to logfile
                        logger.Error(ex, "User is playing new game");

                        return RedirectToAction("Index", "Game");
                    }

                case "HighScores":
                    {
                        //Log to logfile
                        logger.Error(ex, "User is viewing high scores");

                        List<LeaderBoard> dataScores = new List<LeaderBoard>();
                        LeaderBoardService service = new LeaderBoardService();

                        //This commented call exists only for testing purposes. 
                        //service.InsertHighScore(UserManagement.Instance._loggedUser, 251.34m);

                        dataScores = service.GetScores(dataScores);

                        return View("HighScores", dataScores);
                    }

                case "Profile":
                    {
                        //Log to logfile
                        logger.Error(ex, "User is viewing their profile");

                        return View("UserProfile", UserManagement.Instance._loggedUser);
                    }

                case "Logout":
=======
                    return RedirectToAction("Index", "Game");

                case "HighScores":
                    List<LeaderBoard> dataScores = new List<LeaderBoard>();
                    LeaderBoardService service = new LeaderBoardService();

                    //This commented call exists only for testing purposes. 
                    //service.InsertHighScore(UserManagement.Instance._loggedUser, 251.34m);

                    dataScores = service.GetScores(dataScores);

                    return View("HighScores", dataScores);

                case "Profile":
                    return View("UserProfile", UserManagement.Instance._loggedUser);

                case "Logout":
                    Debug.WriteLine("Logged user: " + UserManagement.Instance._loggedUser.UserName);

                    UserManagement.Instance.LogOutUser();

                    if(UserManagement.Instance._loggedUser != null)
>>>>>>> 5fe7e8fabf7ea13a0952032f1716bcbee315e97d
                    {
                        //Log to logfile
                        logger.Error(ex, "User is logging out");

                        Debug.WriteLine("Logged user: " + UserManagement.Instance._loggedUser.UserName);

                        UserManagement.Instance.LogOutUser();

                        if (UserManagement.Instance._loggedUser != null)
                        {
                            Debug.WriteLine("Logged user: " + UserManagement.Instance._loggedUser.UserName);
                        }
                        else
                        {
                            Debug.WriteLine("No user is logged into the application");
                        }
                        return RedirectToAction("Index", "Login");
                    }

                default:
                    {
                        //Log to logfile
                        logger.Error(ex, "User did not select a valid option");

                        break;
                    }
<<<<<<< HEAD
=======
                    return RedirectToAction("Index", "Login");

                default:
                    break;
>>>>>>> 5fe7e8fabf7ea13a0952032f1716bcbee315e97d
            }
            return View("HomePageView");
        }

        private ActionResult ViewGameBoard()
        {
            //Log to logfile
            logger.Error(ex, "User is viewing game board");

            return View("/Views/Game/Game.cshtml");
        }
    }
}