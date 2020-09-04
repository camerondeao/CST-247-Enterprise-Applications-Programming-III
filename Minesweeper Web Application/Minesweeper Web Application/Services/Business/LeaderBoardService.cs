using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Services.Business
{
    public class LeaderBoardService
    {
        LeaderBoardDAO board = new LeaderBoardDAO();

        public List<LeaderBoard> GetScores(List<LeaderBoard> inc)
        {
            return board.GetScores(inc);
        }

        public void InsertHighScore(UserModel user, decimal time)
        {
            board.InsertHighScore(user, time);
        }
    }
}