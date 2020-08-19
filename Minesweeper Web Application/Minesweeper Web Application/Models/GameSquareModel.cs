using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Models
{
    public class GameSquareModel //represents one  game square
    {
        private bool bomb;//does square contain a bomb
        private bool reveal;//has the square been revealed

        public GameSquareModel() {}
        public bool Bomb { get; set; }
        public bool Reveal { get; set; }
    }
}