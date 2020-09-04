using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Models
{
    public class LeaderBoard
    {
        public int Rank { get; set; }
        public string Username { get; set; }
        public decimal Time { get; set; }
    }
}