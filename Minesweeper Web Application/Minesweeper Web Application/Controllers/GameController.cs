﻿using Minesweeper_Web_Application.Models;
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
using NLog;
using Minesweeper_Web_Application.Services.Utility;
using System.Web.Script.Serialization;
using System.Reflection;

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
        public static int mouseClicks;

        public ActionResult Index()
        {
            MineSweeperLogger.GetInstance().Info(new JavaScriptSerializer().Serialize(UserManagement.Instance._loggedUser.UserName + " has started a game"));
            startTime = DateTime.Now;
            squares = new List<GameSquareModel>();
            board = new GameBoardModel(squares, row, col);
            board.PlaceBombs(squares, numofBombs);
            ViewBag.squares = squares;
            board.BombToString(squares);
            Debug.WriteLine(startTime);
            mouseClicks = 0;
            return View("Game");
        }
        public ActionResult OnButtonClick(string mine)
        {
            mouseClicks++;
            int value = Int32.Parse(mine);
            int index = value - 1;
            int r = index / row;
            int c = index % col;
            int squaresRemaining = 0;

            if (squares[index].Bomb == 9)
            {
                MineSweeperLogger.GetInstance().Info(new JavaScriptSerializer().Serialize(UserManagement.Instance._loggedUser.UserName + " has lost a game"));

                ViewBag.squares = squares;
                return View("Loser");
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
                MineSweeperLogger.GetInstance().Info(new JavaScriptSerializer().Serialize(UserManagement.Instance._loggedUser.UserName + " has won a game"));

                TimeSpan elapsed = (DateTime.Now - startTime);
                decimal finalTime = Math.Round((decimal)elapsed.TotalSeconds, 2);

                Debug.WriteLine(String.Format("{0} start time, {1] end time", startTime, DateTime.Now));
                Debug.WriteLine("Difference in time.");
                Debug.WriteLine(String.Format("{0} days, {1} hours, {2} minutes, {3} seconds", elapsed.Days, elapsed.Hours, elapsed.Minutes, elapsed.Seconds));

                LeaderBoardService service = new LeaderBoardService();
                service.InsertHighScore(UserManagement.Instance._loggedUser, finalTime, mouseClicks);

                ViewBag.squares = squares;
                return View("Winner");
            }

            return View("Game");
        }

        //Added this so we have it to post a flag on a right-click of an open space
        public ActionResult OnButtonRightClick(string flagSpace)
        {
            return View("Game");
        }
    }
}