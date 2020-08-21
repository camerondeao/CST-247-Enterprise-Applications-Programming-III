﻿using System;
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

            //Outputting to console for testing purposes.
            BombToString();

            //Calling services to convert to 2D array and place bombs.
            GameplayService service = new GameplayService();
            service.Convert2DArray(gameBoard, 12, 12);
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

            Debug.WriteLine("\nCount of elements in original list: " + gameBoard.Count);
        }

        public void ViewChoice(int row, int col)
        {
            GameplayService service = new GameplayService();
            gameBoard = service.SelectionServices(gameBoard, rowSize, columnSize, row, col);
            //service.ChoiceIteration(gameBoard, row, col);
        }
    }
}