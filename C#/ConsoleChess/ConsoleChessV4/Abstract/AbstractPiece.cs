using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.Abstract
{
    internal abstract class AbstractPiece
    {
        public char PieceIcon { get; set; }

        // how a piece moves
        public int Value { get; set; }
        public int Color { get; set; }
        public int HasMoved { get; set; }
    }
}
