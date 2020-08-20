using Minesweeper_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Minesweeper_Web_Application.Controllers
{
    
    public class GameController : Controller
    {


        public ActionResult Index()
        {
            int row = 12;
            int col = 12;
            int numofBombs = 24;

            GameBoardModel board = new GameBoardModel(row, col);
            
            board.PlaceBombs(numofBombs);
            ViewBag.bombPosition = board.BombToArray();//sends number of surrounding bombs to View
            
            //Test Pictures
            board.SetReveal(0, -1);
            board.SetReveal(1, 0);
            board.SetReveal(2, 1);
            board.SetReveal(3, 2);
            board.SetReveal(4, 3);
            board.SetReveal(5, 4);
            board.SetReveal(6, 5);
            board.SetReveal(7, 6);
            board.SetReveal(9, 7);
            board.SetReveal(8, 8);

            ViewBag.revealPosition = board.RevealToArray();//sends what picture to show in view

            return View("Game");
        }


    }
}