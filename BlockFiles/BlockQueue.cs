using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.BlockFiles.tilePieces
{
    public class BlockQueue
    {
        //create an array of each block type to call from
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new SBlock(),
            new SqrBlock(),
            new TBlock(),
            new ZBlock()
        };

        private readonly Random rand = new Random();

        //get next block to preview in UI
        public Block nextBlock { get; private set; }

       
        //method to get a random block from array
        private Block RandomBlock()
        {
            return blocks[rand.Next(blocks.Length)];
        }
        //add the block to the queue
        public BlockQueue()
        {
            nextBlock = RandomBlock();
        }

        public Block GetandUpdate()
        {
            Block block = nextBlock;

            //returns a new block if the first block id match the previous block
            do
            {
                nextBlock = RandomBlock();
            }
            while (block.id == nextBlock.id);

            return block;
        }
    }
}
