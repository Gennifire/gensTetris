using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Grid
    {
        private readonly int[,] grid;

        public int Rows { get; }
        public int Columns { get; }

        //indexer for accessing array
        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        //
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }

        //Check if  the row and column are inside the grid
        public bool InsideCheck(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c > Columns;
        }

        //check if a row or column inside the grid is empty
        public bool EmptyCheck(int r, int c)
        {
            return InsideCheck(r, c) && grid[r, c] == 0;
        }

        //check if row is full
        public bool CheckRowFull(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //check if row is empty
        public bool CheckRowEmpty(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        //clear row method
        private void ClearRow(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                //simply resets row to zero
                grid[r, c] = 0;
            }
        }

        //move rows down
        private void MoveRowsDown(int r, int NumRows)
        {

        }
    }
}
