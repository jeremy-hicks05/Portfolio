using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.Abstract
{
    public abstract class Piece
    {

        public Piece()
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
