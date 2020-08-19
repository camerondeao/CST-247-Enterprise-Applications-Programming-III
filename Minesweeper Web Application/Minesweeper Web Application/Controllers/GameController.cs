using Minesweeper_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minesweeper_Web_Application.Controllers
{
    
    public class GameController : Controller
    {


        public ActionResult Index()
        {
            
        //    GameBoardModel play = new GameBoardModel();
         //   bool[] array = new bool[play.GetArraySize()];
         //   play.ResetBoard();
          //  ViewBag.bombs = play.PlaceBombs();
            return View("Game");
        }


    }
}