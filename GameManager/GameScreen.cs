using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.GameManager
{
    //grids and grid checks
    public class GameScreen
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
        public GameScreen(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }

        //Check if the row and column are inside the grid
        public bool InsideCheck(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
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
            for (int c = 0; c < Columns; c++)
            {
                grid[r + NumRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }

        public int ClearFullRows()
        {
            //initialise cleared rows to 0
            int cleared = 0;

            //moving from bottom row to top
            for (int r = Rows -1; r >= 0; r--)
            {
                //check if a row is full
                if (CheckRowFull(r))
                {
                    //clear full rows
                    ClearRow(r);
                    //incrementing how many we have cleared
                    cleared++;
                }
                else if (cleared > 0)
                {
                    //move the remaining rows down by the cleared amount
                    MoveRowsDown(r, cleared);
                }
            }

            return cleared;
        }
    }
}
