using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Models
{
    public class GameSquareModel
    {
        public GameSquareModel() { Bomb = 0; Visited = false; }
        public int Bomb { get; set; }
        public int Reveal { get; set; }

        public bool Visited { get; set; }
    }
}