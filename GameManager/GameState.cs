using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.BlockFiles.tilePieces;

namespace Tetris.GameManager
{
    //blocks and block interactions
    public class GameState
    {
        private Block currentBlock;

        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
            }
        }

        public GameScreen gameScreen { get; }
        public BlockQueue blockQueue { get; }
        public bool gameOver { get; private set; }

        public GameState()
        {
            //initialise grid to be 22 by 10
            gameScreen = new GameScreen(22, 10);
            //initialise block queue
            blockQueue = new BlockQueue();
            //use block queue to get current block
            currentBlock = blockQueue.GetandUpdate();
        }

        private bool BlockFitCheck()
        {
            foreach (BlockPosition block in currentBlock.blockPositions())
            {
                //if any blocks are outside the grid or overlapping another tile
                if (!gameScreen.EmptyCheck(block.Row, block.Column))
                {
                    return false;
                }
            }
            return true;
        }

        public void RotateBlockClockWise()
        {
            currentBlock.RotateClockWise();

            if (!BlockFitCheck())
            {
                currentBlock.RotateCounterClockWise();
            }

        }

        public void RotateBlockCounterClockWise()
        {
            currentBlock.RotateCounterClockWise();

            if (!BlockFitCheck())
            {
                currentBlock.RotateClockWise();
            }

        }


        public void MoveBlockLeft()
        {
            currentBlock.MoveState(0, -1);

            if (!BlockFitCheck())
            {
                currentBlock.MoveState(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            currentBlock.MoveState(0, 1);

            if (!BlockFitCheck())
            {
                currentBlock.MoveState(0, -1);
            }
        }

        
        private bool GameOverCheck()
        {
            //if either of the top offset rows are not empty, game is over
            return !(gameScreen.CheckRowFull(0) && gameScreen.CheckRowFull(1));
        }

        private void PutBlock()
        {
            //loop over tile position of current block
            foreach(BlockPosition block in currentBlock.blockPositions())
            {
                //sets position within the grid to the current block id
                gameScreen[block.Row, block.Column] = currentBlock.id;
            }

            //if any rows are full, clear them
            gameScreen.ClearFullRows();

            //check if game is over
            if (GameOverCheck())
            {
                gameOver = true;
            }
            else
            {
                //if game not over update current block
                currentBlock = blockQueue.GetandUpdate();
            }
        }

        public void MoveBlockDown()
        {
            currentBlock.MoveState(1, 0);

            if (!BlockFitCheck())
            {
                currentBlock.MoveState(-1, 0);
                //calls put block incase block cannot be moved down
                PutBlock();
            }
        }
    }
}
