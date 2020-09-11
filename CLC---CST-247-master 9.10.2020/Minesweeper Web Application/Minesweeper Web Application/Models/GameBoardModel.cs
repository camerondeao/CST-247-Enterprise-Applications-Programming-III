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
using System.Diagnostics;
using Minesweeper_Web_Application.Services.Gameplay;
using System.EnterpriseServices;
using System.Web.UI;

namespace Minesweeper_Web_Application.Models
{
    public class GameBoardModel
    {
        private int rowSize;
        private int columnSize;

        public GameBoardModel(List<GameSquareModel> gameBoard, int r, int c)
        {
            GameSquareModel temp = new GameSquareModel();
            rowSize = r;
            columnSize = c;
            for (int i = 0; i < r * c; i++)
            {
                temp = new GameSquareModel();
                gameBoard.Add(temp);
            }

        }

        public int RowSize {get; set;}
        public int ColumnSize { get; set; }
        public int GetBoardSize()
        {
            return rowSize * columnSize;
        }

        public void PlaceBombs(List<GameSquareModel> gameBoard, int n)//set locations of bombs
        {
            Random random = new Random();

            for (int i = 0; i < n; i++)//Create an array of random positions to place boms in an one-dimensional array
            {
                int position = random.Next(1, GetBoardSize());
                 while (gameBoard[position].Bomb == 9)
                 {
                      position = random.Next(1, GetBoardSize());
                  }
                gameBoard[position].Bomb = 9;
            }

            GameplayService service = new GameplayService();
            service.Convert2DArray(gameBoard, 12, 12);
        }
        public void SetReveal(List<GameSquareModel> gameBoard, int i, int e)//resets are square boolean reveal to false
        {   
                    gameBoard[i].Reveal = e;
        }
        public bool GetVisited(List<GameSquareModel> gameBoard, int i)//resets are square boolean reveal to false
        {
            return gameBoard[i].Visited;
        }
        public void SetVisited(List<GameSquareModel> gameBoard, int i)//resets are square boolean reveal to false
        {
            gameBoard[i].Visited = !gameBoard[i].Visited;
        }
        public void ResetReveal(List<GameSquareModel> gameBoard)//resets are square boolean reveal to false
        {
            for (int i = 0; i < GetBoardSize(); i++)
            {
                {
                    gameBoard[i].Visited = false;
                }
            }
        }
        public int[] BombToArray(List<GameSquareModel> gameBoard)
        {
            int[] array = new int[GetBoardSize()];
            for (int i = 0; i < GetBoardSize(); i++)
            {
                array[i] = gameBoard[i].Bomb;
            }
            return array;
        }
        public bool[] VisitedToArray(List<GameSquareModel> gameBoard)
        {
            bool[] array = new bool[GetBoardSize()];
            for (int i = 0; i < GetBoardSize(); i++)
            {
                array[i] = gameBoard[i].Visited;
            }
            return array;
        }
        public int[] RevealToArray(List<GameSquareModel> gameBoard)
        {
            int[] array = new int[GetBoardSize()];
            for (int i = 0; i < GetBoardSize(); i++)
            {
                array[i] = gameBoard[i].Reveal;
            }
            return array;
        }

        public void BombToString(List<GameSquareModel> gameBoard)
        {
            int count = 0;
            Debug.Write("\n\n\n");
            Debug.WriteLine("Original List<GameSquareModel> board\n");
            for (int i = 0; i < GetBoardSize(); i++)
            {
                Debug.Write(gameBoard[i].Bomb + " ");
                if (++count == rowSize)
                {
                    Debug.Write("\n");
                    count = 0;
                }
            }
            if (count > 0)
            {
                Debug.Write("\n");
            }
        }

        public void ViewChoice(List<GameSquareModel> gameBoard, int row, int col)
        {
            GameplayService service = new GameplayService();
            gameBoard = service.SelectionServices(gameBoard, rowSize, columnSize, row, col);
        }
    }
}