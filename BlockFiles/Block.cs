using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public abstract class Block
    {
        //2d array to contain tile positions in rotation states
        protected abstract BlockPosition[][]  Tiles{ get; }
        //start offset to decide where tile spawns within the grid
        protected abstract BlockPosition StartOffSet { get; }
        //id to distinguish blocks
        public abstract int id { get; }

        //store current rotation state
        private int rotationState;
        //store current offset
        private BlockPosition offSet;


        //constructor sets offset to equal the start offset
        public Block()
        {
            offSet = new BlockPosition(StartOffSet.Row, StartOffSet.Column);
        }

        //returns current position of the blocks factoring in current rotation & offset
        public IEnumerable<BlockPosition> BlockPositions()
        {
            foreach (BlockPosition block in Tiles[rotationState])
            {
                yield return new BlockPosition(block.Row + offSet.Row, block.Column + offSet.Column);
            }
        }

        public void RotateClockWise()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        public void RotateCounterClockWise()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }
        //moves tiles by n rows / columns
        public void MoveState(int rows, int columns)
        {
            offSet.Row += rows;
            offSet.Column += columns;
        }

        //resets tile to initial start position
        public void Reset()
        {
            rotationState = 0;
            offSet.Row = StartOffSet.Row;
            offSet.Column = StartOffSet.Column;
        }

    }
}
