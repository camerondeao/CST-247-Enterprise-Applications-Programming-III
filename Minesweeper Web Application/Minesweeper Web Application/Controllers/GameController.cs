using Minesweeper_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Diagnostics;
using System.Linq.Expressions;
using Minesweeper_Web_Application.Services.Business;
<<<<<<< HEAD
using NLog;
=======
>>>>>>> 5fe7e8fabf7ea13a0952032f1716bcbee315e97d

namespace Minesweeper_Web_Application.Controllers
{
    
    public class GameController : Controller
    {
        public static int row = 12;
        public static int col = 12;
        public static int numofBombs = 24;
        public static int safeSquares = (row * col - numofBombs);
        public static GameBoardModel board = null;
        public static List<GameSquareModel> squares = null;
        public static DateTime startTime;

<<<<<<< HEAD
        //Logger variables
        Logger logger = LogManager.GetLogger("fileLogger");
        readonly ArithmeticException ex;

        public ActionResult Index()
        {
            //Log to logfile
            logger.Error(ex, "User started new game");

=======
        public ActionResult Index()
        {
>>>>>>> 5fe7e8fabf7ea13a0952032f1716bcbee315e97d
            startTime = DateTime.Now;
            squares = new List<GameSquareModel>();
            board = new GameBoardModel(squares, row, col);
            board.PlaceBombs(squares, numofBombs);
            ViewBag.squares = squares;
            board.BombToString(squares);
            Debug.WriteLine(startTime);
            return View("Game");
        }
        public ActionResult OnButtonClick(string mine)
        {
            int value = Int32.Parse(mine);
            int index = value - 1;
            int r = index / row;
            int c = index % col;
            int squaresRemaining = 0;

            if (squares[index].Bomb == 9)
            {
                //Log to logfile
                logger.Error(ex, "User lost game");

                ViewBag.squares = squares;
                return View("Loser");
            }
            else
            {
                //Log to logfile
                logger.Error(ex, "User made move");

                board.ViewChoice(squares, r, c);
                ViewBag.squares = squares;
            }

            //Log to logfile
            logger.Error(ex, "Calculating remaining spaces");

            for (int i = 0; i < row * col; i++)
            {
                if (!squares[i].Visited)
                { 
                    squaresRemaining++;
                    Debug.WriteLine("Squares remaining: " + squaresRemaining);
                }
            }

            if(squaresRemaining == numofBombs)
            {
<<<<<<< HEAD
                //Log to logfile
                logger.Error(ex, "User won game");

                TimeSpan elapsed = (DateTime.Now - startTime);
                decimal finalTime = Math.Round((decimal)elapsed.TotalSeconds, 2);

                //This line gave me an error when testing the Logging services -- FYI
                //Not sure if it has to do with backspacing to complete the board or not
=======
                TimeSpan elapsed = (DateTime.Now - startTime);
                decimal finalTime = Math.Round((decimal)elapsed.TotalSeconds, 2);

>>>>>>> 5fe7e8fabf7ea13a0952032f1716bcbee315e97d
                Debug.WriteLine(String.Format("{0} start time, {1] end time", startTime, DateTime.Now));
                Debug.WriteLine("Difference in time.");
                Debug.WriteLine(String.Format("{0} days, {1} hours, {2} minutes, {3} seconds", elapsed.Days, elapsed.Hours, elapsed.Minutes, elapsed.Seconds));

                LeaderBoardService service = new LeaderBoardService();
                service.InsertHighScore(UserManagement.Instance._loggedUser, finalTime);

                ViewBag.squares = squares;
                return View("Winner");
            }

            return View("Game");
        }

        //Added this so we have it to post a flag on a right-click of an open space
        public ActionResult OnButtonRightClick(string flagSpace)
        {
            //Log to logfile
            logger.Error(ex, "User right-clicked on space");

            return View("Game");
        }
    }
}