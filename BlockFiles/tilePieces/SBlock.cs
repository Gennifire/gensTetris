﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.BlockFiles.tilePieces
{
    class SBlock : Block
    {
        
        protected override BlockPosition[][] Tiles => new BlockPosition[][] {
            new BlockPosition[] { new(0,1), new(0,2), new(1,0), new(1,1) },
            new BlockPosition[] { new(0,1), new(1,1), new(1,2), new(2,2) },
            new BlockPosition[] { new(1,1), new(1,2), new(2,0), new(2,1) },
            new BlockPosition[] { new(0,0), new(1,0), new(1,1), new(2,1) }
        };

        public override int id => 5;

        protected override BlockPosition StartOffSet => new(0, 3);
    }
}
