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
        public long SaveBomb1 { get; set; }
        public long SaveBomb2 { get; set; }
        public long SaveBomb3 { get; set; }
        public long SaveBomb4 { get; set; }
        public long SaveVisited1 { get; set; }
        public long SaveVisited2 { get; set; }
        public long SaveVisited3 { get; set; }
        public long SaveVisited4 { get; set; }

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

        public int RowSize { get; set; }
        public int ColumnSize { get; set; }
        public int GetBoardSize()
        {
            return rowSize * columnSize;
        }
        public int GetSaveColumn()
        {
            return GetBoardSize() / 4;
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
        public void ConvertBombToInt(GameBoardModel board, List<GameSquareModel> gameBoard)
        {
            int count = 1;
            int cols = gameBoard.Count / 4;
            long num = 0;
            board.SaveBomb1 = 0;
            board.SaveBomb2 = 0;
            board.SaveBomb3 = 0;
            board.SaveBomb4 = 0;

            for (int i = 0; i < cols; i++)
            {
                if (gameBoard[i].Bomb == -1)
                {
                    num += (long)Math.Pow(2, count);
                    board.SaveBomb1 += (long)Math.Pow(2, count);
                }
                count++;
            }
            num = 0;
            count = 1;
            for (int i = cols; i < cols * 2; i++)
            {
                if (gameBoard[i].Bomb == -1)
                {
                    num += (long)Math.Pow(2, count);
                    board.SaveBomb2 += (long)Math.Pow(2, count);
                }
                count++;
            }
            num = 0;
            count = 1;
            for (int i = cols * 2; i < cols * 3; i++)
            {
                if (gameBoard[i].Bomb == -1)
                {
                    board.SaveBomb3 += (long)Math.Pow(2, count);
                }
                count++;
            }
            num = 0;
            count = 1;
            for (int i = cols * 3; i < cols * 4; i++)
            {
                if (gameBoard[i].Bomb == -1)
                {
                    board.SaveBomb4 += (long)Math.Pow(2, count);
                }
                count++;
            }
        }
        public void ConvertBombToList(GameBoardModel board, List<GameSquareModel> gameBoard)
        {
            List<int> binary = new List<int>();
            long num = board.SaveBomb1;
            int rem = (int)num % 2;
            int j = 0;

            while (j < board.GetSaveColumn())
            {
                if (num > 0)
                {
                    num /= 2;
                    rem = (int)num % 2;
                    binary.Add(rem);
                }
                else if (j < board.GetSaveColumn())
                {
                    binary.Add(0);
                }
                j++;
            }
            Console.WriteLine("SaveBomb 2");
            j = 0;
            num = board.SaveBomb2;
            while (j < board.GetSaveColumn())
            {
                if (num > 0)
                {
                    num /= 2;
                    rem = (int)num % 2;
                    binary.Add(rem);
                }
                else if (j < board.GetSaveColumn())
                {
                    binary.Add(0);
                }
                j++;

            }
            j = 0;
            num = board.SaveBomb3;
            while (j < board.GetSaveColumn())
            {
                if (num > 0)
                {
                    num /= 2;
                    rem = (int)num % 2;
                    binary.Add(rem);
                }
                else
                {
                    binary.Add(0);
                }
                j++;

            }
            j = 0;
            num = board.SaveBomb4;
            while (j < board.GetSaveColumn())
            {
                if (num > 0)
                {
                    num /= 2;
                    rem = (int)num % 2;
                    binary.Add(rem);
                }
                else if (j < board.GetSaveColumn())
                {
                    binary.Add(0);
                }
                j++;

            }
            for (int i = 0; i < binary.Count; i++)
            {
                if (binary[i] == 1)
                {
                    gameBoard[i].Bomb = -1;
                }
                else
                {
                    gameBoard[i].Bomb = 0;
                }
            }
        }
        public void ConvertVisitedToInt(GameBoardModel board, List<GameSquareModel> gameBoard)
        {
            int count = 1;
            int cols = gameBoard.Count / 4;
            long num = 0;
            board.SaveVisited1 = 0;
            board.SaveVisited2 = 0;
            board.SaveVisited3 = 0;
            board.SaveVisited4 = 0;

            for (int i = 0; i < cols; i++)
            {
                if (gameBoard[i].Bomb == -1)
                {
                    num += (long)Math.Pow(2, count);
                    board.SaveVisited1 += (long)Math.Pow(2, count);
                }
                count++;
            }
            num = 0;
            count = 1;
            for (int i = cols; i < cols * 2; i++)
            {
                if (gameBoard[i].Bomb == -1)
                {
                    num += (long)Math.Pow(2, count);
                    board.SaveBomb2 += (long)Math.Pow(2, count);
                }
                count++;
            }
            num = 0;
            count = 1;
            for (int i = cols * 2; i < cols * 3; i++)
            {
                if (gameBoard[i].Bomb == -1)
                {
                    board.SaveVisited3 += (long)Math.Pow(2, count);
                }
                count++;
            }
            num = 0;
            count = 1;
            for (int i = cols * 3; i < cols * 4; i++)
            {
                if (gameBoard[i].Bomb == -1)
                {
                    board.SaveVisited4 += (long)Math.Pow(2, count);
                }
                count++;
            }
        }
        public void ConvertVisitedToList(GameBoardModel board, List<GameSquareModel> gameBoard)
        {
            List<int> binary = new List<int>();
            long num = board.SaveVisited1;
            int rem = (int)num % 2;
            int j = 0;

            while (j < board.GetSaveColumn())
            {
                if (num > 0)
                {
                    num /= 2;
                    rem = (int)num % 2;
                    binary.Add(rem);
                }
                else if (j < board.GetSaveColumn())
                {
                    binary.Add(0);
                }
                j++;
            }
            j = 0;
            num = board.SaveVisited2;
            while (j < board.GetSaveColumn())
            {
                if (num > 0)
                {
                    num /= 2;
                    rem = (int)num % 2;
                    binary.Add(rem);
                }
                else if (j < board.GetSaveColumn())
                {
                    binary.Add(0);
                }
                j++;

            }
            j = 0;
            num = board.SaveVisited3;
            while (j < board.GetSaveColumn())
            {
                if (num > 0)
                {
                    num /= 2;
                    rem = (int)num % 2;
                    binary.Add(rem);
                }
                else
                {
                    binary.Add(0);
                }
                j++;

            }
            j = 0;
            num = board.SaveVisited4;
            while (j < board.GetSaveColumn())
            {
                if (num > 0)
                {
                    num /= 2;
                    rem = (int)num % 2;
                    binary.Add(rem);
                }
                else if (j < board.GetSaveColumn())
                {
                    binary.Add(0);
                }
                j++;

            }
            for (int i = 0; i < binary.Count; i++)
            {
                if (binary[i] == 1)
                {
                    gameBoard[i].Visited = true;
                }
                else
                {
                    gameBoard[i].Visited = false;
                }
            }
        }
    }
}