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
    }
}
