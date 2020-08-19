using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace Minesweeper_Web_Application.Models
{
    public class GameBoardModel
    {
        
        private const int rowSize = 12;
        private const int columnSize = 12;
     //   private bool bomb;//does square contain a bomb
      //  private bool reveal;//has the square been revealed

        public bool[] bombPosition = new bool[rowSize*columnSize];//array position of placed bombs
        public bool[] revealPosition = new bool[rowSize*columnSize];//array position of placed bombs


        public int RowSize { get; set; }
        public int ColumnSize { get; set; }
        public bool Bomb { get; set; }
        public bool Reveal { get; set; }

        public GameBoardModel(){
            ResetBoard();
        }
        public int GetArraySize() {
            return rowSize * columnSize;
        }
        public void ResetBoard()//place new bombs.  Changes all Game squares reveals to false
        {
            ResetReveal();//resets are square boolean reveal to false
            PlaceBombs();
        }
        public void ResetReveal()//resets are square boolean reveal to false
        {
            for (int i = 0; i < GetArraySize(); i++) {

                {
                    revealPosition[i] = false;
                }
            }
        }
        public bool[] PlaceBombs()//set locations of bombs
        {
            var array = Enumerable.Repeat<bool>(false, GetArraySize()).ToArray();
            Random random = new Random();
            int position;

            for (int i=0; i< GetArraySize(); i++)//Create an array of random positions to place boms in an one-dimensional array
            {
                position = random.Next(1, GetArraySize());
                if (array[position] == false) { array[position] = true; }
                else { i--; }
            }
            Array.Copy(array, bombPosition, GetArraySize());
            return array;
        }

    }
}