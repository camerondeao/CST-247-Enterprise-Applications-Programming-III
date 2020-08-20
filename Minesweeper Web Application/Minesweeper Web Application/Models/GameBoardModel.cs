using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using System.Drawing;
using Image = System.Drawing.Image;

namespace Minesweeper_Web_Application.Models
{
    public class GameBoardModel
    {
        List<GameSquareModel> gameBoard = new List<GameSquareModel>();
        int rowSize;
        int columnSize;

        public GameBoardModel(int r, int c)
        {
            GameSquareModel temp = new GameSquareModel();
            rowSize = r;
            columnSize = c;
            for (int i = 0; i < r * c; i++)
            {
                temp = new GameSquareModel();
                gameBoard.Add(temp);
                
            }
            ResetReveal();
        }
        public int GetBoardSize()
        {
            return rowSize * columnSize;
        }

        public void PlaceBombs(int n)//set locations of bombs
        {
            Random random = new Random();
            for (int i = 0; i < n; i++)//Create an array of random positions to place boms in an one-dimensional array
            {
                int position = random.Next(1, GetBoardSize());
                // while (gameBoard[position].Bomb == -1)
                // {
                //      position = random.Next(1, GetBoardSize());
                //  }
                // Console.Write(position + " ");
                gameBoard[position].Bomb = -1;
            }
        }
        public void SetReveal(int i, int e)//resets are square boolean reveal to false
        {   
                    gameBoard[i].Reveal = e;
        }
        public void ResetReveal()//resets are square boolean reveal to false
        {
            for (int i = 0; i < GetBoardSize(); i++)
            {

                {
                    gameBoard[i].Reveal = 9;
                }
            }
        }
        public int[] BombToArray()
        {
            int[] array = new int[GetBoardSize()];
            for (int i = 0; i < GetBoardSize(); i++)
            {
                array[i] = gameBoard[i].Bomb;
            }
            return array;
        }
        public int[] RevealToArray()
        {
            int[] array = new int[GetBoardSize()];
            for (int i = 0; i < GetBoardSize(); i++)
            {
                array[i] = gameBoard[i].Reveal;
            }
            return array;
        }

        public void BombToString()
        {
            int count = 0;
            Console.Write("\n\n\n");
            for (int i = 0; i < GetBoardSize(); i++)
            {
                Console.Write(gameBoard[i].Bomb + " ");
                if (++count == rowSize)
                {
                    Console.Write("\n");
                    count = 0;
                }
            }
            if (count > 0)
            {
                Console.Write("\n");
            }
        }
       


    }
}