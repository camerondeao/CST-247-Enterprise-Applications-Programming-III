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
using NLog;
using Minesweeper_Web_Application.Services.Utility;
using System.Web.Script.Serialization;
using System.Reflection;
using Minesweeper_Web_Application.Services.Gameplay;

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
        public static DateTime startTime;
        public static int mouseClicks;

        public ActionResult Index()
        {
            MineSweeperLogger.GetInstance().Info(String.Format("{0} has started a game.", UserManagement.Instance._loggedUser.UserName));
            startTime = DateTime.Now;
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
            mouseClicks = (int)values[8];

            Debug.WriteLine("INC MOUSE CLICKS: " + mouseClicks);
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

            Debug.WriteLine(startTime);
            //mouseClicks = 0;

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
        }

        [HttpPost]
        public PartialViewResult OnButtonClick(string mine)
        {
            mouseClicks++;

            int value = Int32.Parse(mine);
            int index = value - 1;
            int r = index / row;
            int c = index % col;
            int squaresRemaining = 0;

            if (squares[index].Bomb == 9)
            {
                MineSweeperLogger.GetInstance().Info(String.Format("{0} has lost a game.", UserManagement.Instance._loggedUser.UserName));
                ViewBag.squares = squares;
                //return View("Loser");
                EndGame("Lose");
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
                MineSweeperLogger.GetInstance().Info(String.Format("{0} has won a game.", UserManagement.Instance._loggedUser.UserName));

                TimeSpan elapsed = (DateTime.Now - startTime);
                decimal finalTime = Math.Round((decimal)elapsed.TotalSeconds, 2);

                Debug.WriteLine(String.Format("{0} start time, {1} end time", startTime, DateTime.Now));
                Debug.WriteLine("Difference in time.");
                Debug.WriteLine(String.Format("{0} days, {1} hours, {2} minutes, {3} seconds", elapsed.Days, elapsed.Hours, elapsed.Minutes, elapsed.Seconds));

                LeaderBoardService service = new LeaderBoardService();
                service.InsertHighScore(UserManagement.Instance._loggedUser, finalTime, mouseClicks);
                EndGame("Win");
            }

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

            GameDAO gameDAO = new GameDAO();
            gameDAO.SaveGameBombs(board, UserManagement.Instance._loggedUser, mouseClicks);
            
            return RedirectToAction("MainPage", "Home");
        }
    }
}