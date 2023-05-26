using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.Abstract
{
    internal abstract class AbstractPiece
    {

        public AbstractPiece()
        {
            HasMoved = false;
        }
        public char PieceIcon { get; set; }

        // how a piece moves
        public int Value { get; set; }
        public bool Color { get; set; }
        public bool HasMoved { get; set; }
    }
}
