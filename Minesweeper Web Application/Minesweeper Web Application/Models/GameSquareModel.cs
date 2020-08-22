using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Models
{
    public class GameSquareModel
    {
        public int bomb;//(0-8):numbers of bombs in adjacent squares:


        //picture to be displayed 
        public int reveal; //-1: Bomb
                            //0: Empty
                            //(0-8):Adjacent bombs
                            //9: hidden
        public bool visited;


        public GameSquareModel() { bomb = 0; reveal = 9; visited = false; }
        public int Bomb { get; set; }
        public int Reveal { get; set; }

        public bool Visited { get; set; }

    }
}