using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.BlockFiles.tilePieces
{
    class IBlock : Block
    {
        private readonly BlockPosition[][] tiles = new BlockPosition[][]
        {
            new BlockPosition[] { new(1,0), new(1,1), new(1,2), new(1,3) },
            new BlockPosition[] { new(0,2), new(1,2), new(2,2), new(3,2) },
            new BlockPosition[] { new(1,0), new(1,1), new(1,2), new(1,3) },
            new BlockPosition[] { new(1,0), new(1,1), new(1,2), new(1,3) }
        };

        public override int id => 1;
        protected override BlockPosition StartOffSet => new BlockPosition(-1,3);
        protected override BlockPosition[][] Tiles => tiles;
    }
}
