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
        public ActionResult Index()
        {
            squares = new List<GameSquareModel>();
            board = new GameBoardModel(squares, row, col);
            board.PlaceBombs(squares, numofBombs);
            ViewBag.squares = squares;
            board.BombToString(squares);
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