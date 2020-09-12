using Minesweeper_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Minesweeper_Web_Application.Services.Gameplay
{
    public class GameplayService
    {
        public void Convert2DArray(List<GameSquareModel> input, int height, int width)
        {
            Debug.Write("\n\n");
            Debug.Write("Converting to 2D array.\n");
            GameSquareModel[,] output = new GameSquareModel[height, width];
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }

            SetNeighborValue(output);
        }

        private void SetNeighborValue(GameSquareModel[,] board)
        {
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int x = 0; x < board.GetLength(1); x++)
                {
                    int neighborCounter = 0;

                    if(i > 0 && board[i - 1, x].Bomb == 9)
                    {
                        neighborCounter++;
                    }
                    if (x > 0 && board[i, x - 1].Bomb == 9)
                    {
                        neighborCounter++;
                    }
                    if(i > 0 && x > 0 && board[i - 1, x - 1].Bomb == 9)
                    {
                        neighborCounter++;
                    }
                    if(i < board.GetLength(0) - 1 && board[i + 1, x].Bomb == 9)
                    {
                        neighborCounter++;
                    }
                    if(x < board.GetLength(0) - 1 && board[i, x + 1].Bomb == 9)
                    {
                        neighborCounter++;
                    }
                    if(i < board.GetLength(0) - 1 && x < board.GetLength(0) - 1 && board[i + 1, x + 1].Bomb == 9)
                    {
                        neighborCounter++;
                    }
                    if(i < board.GetLength(0) - 1 && x > 0 && board[i + 1, x - 1].Bomb == 9)
                    {
                        neighborCounter++;
                    }
                    if(i > 0 && x < board.GetLength(0) - 1 && board[i - 1, x + 1].Bomb == 9)
                    {
                        neighborCounter++;
                    }

                    board[i, x].Reveal = neighborCounter;
                }
            }
        }

        public List<GameSquareModel> ConvertToList(GameSquareModel[,] input)
        {
            List<GameSquareModel> conversion = new List<GameSquareModel>();

            for(int i = 0; i < input.GetLength(0); i++)
            {
                for(int j = 0; j < input.GetLength(1); j++)
                {
                    conversion.Add(input[i, j]);
                }
            }

            Debug.Write(Environment.NewLine);
            Debug.Write("Displaying the conversion of a 2D array into a List<GameSquareModel>\n");

            int count = 0;
            for(int i = 0; i < 12 * 12; i++)
            {
                Debug.Write(conversion[i].Bomb + " ");
                if(++count == 12)
                {
                    Debug.Write("\n");
                    count = 0;
                }
            }
            if(count > 0)
            {
                Debug.Write("\n");
            }
            Debug.Write("\nCount of elements in list conversion: " + conversion.Count + "\n\n");
            return null;
        }

        public List<GameSquareModel> SelectionServices(List<GameSquareModel> input, int height, int width, int rowChoice, int colChoice)
        {
            GameSquareModel[,] output = new GameSquareModel[height,width];

            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    //Debug.WriteLine("Variable Check.");
                    //Debug.WriteLine("height width i j ");
                    //Debug.WriteLine(height + width + " " + i + " " + j );
                    output[i, j] = input[i * width + j];
                }
            }

            output = ChoiceIteration(output, rowChoice, colChoice);
            return ConvertToList(output);
        }

        public GameSquareModel[,] ChoiceIteration(GameSquareModel[,] input, int row, int col)
        {
            //Debug.WriteLine("Firing ChoiceIteration method. Row: " + row + " Col: " + col);
            if(input[row, col].Reveal == 0 && input[row, col].Visited == false)
            {
                input[row, col].Visited = true;

                if(row - 1 >= 0 && input[row - 1, col].Bomb != 9)
                {
                    ChoiceIteration(input, row - 1, col);
                }
                if(row + 1 < input.GetLength(0) && input[row + 1, col].Bomb != 9)
                {
                    ChoiceIteration(input, row + 1, col);
                }
                if(col + 1 < input.GetLength(1) && input[row, col + 1].Bomb != 9)
                {
                    ChoiceIteration(input, row, col + 1);
                }
                if(col - 1 >= 0 && input[row, col - 1].Bomb != 9)
                {
                    ChoiceIteration(input, row, col - 1);
                }
            }
            else
            {
                input[row, col].Visited = true;
            }

            return input;
        }

        private void ConsoleBoardDisplay(GameSquareModel[,] input)
        {
            Debug.WriteLine("Displaying 2D array board.");
            Debug.WriteLine("List: " + input.ToString());
            Debug.Write(Environment.NewLine);

            int height = input.GetLength(0);
            int width = input.GetLength(1);
            int headerCounter = 0;
            int sideCounter = 0;
            int elementCounter = 0;

            Debug.Write("   ");

            for(int i = 0; i < height; i++)
            {
                Debug.Write(headerCounter + " ");
                headerCounter++;
            }

            Debug.Write(Environment.NewLine);

            for(int i = 0; i < height; i++)
            {
                if(sideCounter < 10)
                {
                    Debug.Write(" " + sideCounter + "|");
                }
                else
                {
                    Debug.Write(sideCounter + "|");
                }
                
                for(int j = 0; j < width; j++)
                {
                    Debug.Write(input[i, j].Bomb + " ");
                    elementCounter++;
                }
                Debug.Write(Environment.NewLine);
                sideCounter++;
            }
            Debug.Write("\nCount of elements in 2D array conversion: " + elementCounter + "\n\n");
        }
    }
}