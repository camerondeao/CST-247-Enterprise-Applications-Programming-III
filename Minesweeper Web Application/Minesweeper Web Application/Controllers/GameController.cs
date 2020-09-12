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
using Minesweeper_Web_Application.Services.Data;
using Minesweeper_Web_Application.Services.Business;
<<<<<<< HEAD
using NLog;
using Minesweeper_Web_Application.Services.Utility;
using System.Web.Script.Serialization;
using System.Reflection;
=======
using Minesweeper_Web_Application.Services.Gameplay;
>>>>>>> Mywork4

namespace Minesweeper_Web_Application.Controllers
{
    
    public class GameController : Controller
    {
        public static int row = 12;
        public static int col = 12;
        public static int numofBombs = 12;
        public static int safeSquares = (row * col - numofBombs);
        public static GameBoardModel board = null;
        public static List<GameSquareModel> squares = null;
<<<<<<< HEAD
        public static DateTime startTime;
        public static int mouseClicks;

        public ActionResult Index()
        {
            MineSweeperLogger.GetInstance().Info(new JavaScriptSerializer().Serialize(UserManagement.Instance._loggedUser.UserName + " has started a game"));
            startTime = DateTime.Now;
=======

        public ActionResult Index()
        {
>>>>>>> Mywork4
            squares = new List<GameSquareModel>();
            board = new GameBoardModel(squares, row, col);
            board.PlaceBombs(squares, numofBombs);
            board.BombToString(squares);
            ViewBag.squares = squares;
            return View("Game", squares);
        }

        public ActionResult LoadGame()
        {
            GameDAO gameDAO = new GameDAO();
            List<long> values = new List<long>();
            squares = new List<GameSquareModel>();
            board = new GameBoardModel(squares, row, col);

            values = gameDAO.RetrieveUserScore(values, UserManagement.Instance._loggedUser);

            board.SaveBomb1 = values[0];
            board.SaveBomb2 = values[1];
            board.SaveBomb3 = values[2];
            board.SaveBomb4 = values[3];

            board.SaveVisited1 = values[4];
            board.SaveVisited2 = values[5];
            board.SaveVisited3 = values[6];
            board.SaveVisited4 = values[7];

            board.ConvertBombToList(board, squares);
            board.ConvertVisitedToList(board, squares);

            int num = 0;
            for (int i = 0; i < squares.Count; i++)
            {
                if (!squares[i].Visited)
                {
                    num++;
                }
            }
            Debug.WriteLine("LOADED REMAINING CELLS! " + num);

            GameplayService service = new GameplayService();
            service.Convert2DArray(squares, 12, 12);
            board.BombToString(squares);
<<<<<<< HEAD
            Debug.WriteLine(startTime);
            mouseClicks = 0;
            return View("Game");
=======

            Debug.WriteLine("Displaying board");
            int count = 0;
            for (int i = 0; i < 12 * 12; i++)
            {
                Debug.Write(squares[i].Visited + " ");
                if (++count == 12)
                {
                    Debug.Write("\n");
                    count = 0;
                }
            }
            ViewBag.squares = squares;
            return View("Game", squares);
>>>>>>> Mywork4
        }

        [HttpPost]
        public PartialViewResult OnButtonClick(string mine)
        {
<<<<<<< HEAD
            mouseClicks++;
=======

>>>>>>> Mywork4
            int value = Int32.Parse(mine);
            int index = value - 1;
            int r = index / row;
            int c = index % col;
            int squaresRemaining = 0;

            if (squares[index].Bomb == 9)
            {
<<<<<<< HEAD
                MineSweeperLogger.GetInstance().Info(new JavaScriptSerializer().Serialize(UserManagement.Instance._loggedUser.UserName + " has lost a game"));

                ViewBag.squares = squares;
                return View("Loser");
=======
                EndGame("Lose");
>>>>>>> Mywork4
            }
            else
            {
                board.ViewChoice(squares, r, c);
                ViewBag.squares = squares;
            }

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
                MineSweeperLogger.GetInstance().Info(new JavaScriptSerializer().Serialize(UserManagement.Instance._loggedUser.UserName + " has won a game"));

                TimeSpan elapsed = (DateTime.Now - startTime);
                decimal finalTime = Math.Round((decimal)elapsed.TotalSeconds, 2);

                Debug.WriteLine(String.Format("{0} start time, {1] end time", startTime, DateTime.Now));
                Debug.WriteLine("Difference in time.");
                Debug.WriteLine(String.Format("{0} days, {1} hours, {2} minutes, {3} seconds", elapsed.Days, elapsed.Hours, elapsed.Minutes, elapsed.Seconds));

                LeaderBoardService service = new LeaderBoardService();
                service.InsertHighScore(UserManagement.Instance._loggedUser, finalTime, mouseClicks);
=======
                EndGame("Win");
            }
>>>>>>> Mywork4

            return PartialView("_UpdateGame", squares);
        }
        public ActionResult EndGame(string finish)
        {
            GameDAO gameDAO = new GameDAO();
            gameDAO.DeleteScore(UserManagement.Instance._loggedUser);
            if (finish == "Win")
            {
                return View("Winner");
            }
            else {
                return View("Loser");
            }
        }

        public ActionResult SaveGame()
        {
            int num = 0;
            for(int i = 0; i < squares.Count; i++)
            {
                if(!squares[i].Visited)
                {
                    num++;
                }
            }

            Debug.WriteLine("REMAINING CELLS! " + num);

            //Debug.WriteLine("BEFORE CONVERSION: " + board.SaveBomb1 + board.SaveBomb2 + board.SaveBomb3 + board.SaveBomb4
            //   + board.SaveVisited1 + board.SaveVisited2 + board.SaveVisited3 + board.SaveVisited4);


            Debug.WriteLine("Displaying board");
            int count = 0;
            for (int i = 0; i < 12 * 12; i++)
            {
                Debug.Write(squares[i].Visited + " ");
                if (++count == 12)
                {
                    Debug.Write("\n");
                    count = 0;
                }
            }

            Debug.WriteLine(Environment.NewLine);
            board.ConvertBombToInt(board, squares);
            board.ConvertVisitedToInt(board, squares);

            GameDAO testing = new GameDAO();
            testing.SaveGameBombs(board, UserManagement.Instance._loggedUser);
            
            return RedirectToAction("MainPage", "Home");
        }
    }
}