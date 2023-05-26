using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.Board
{
    internal class ChessBoardSpace
    {
        public Abstract.AbstractPiece? Piece { get; set; }

        public bool HasAPiece()
        {
            return this.Piece != null;
        }

        public bool IsEmpty()
        {
            return this.Piece == null;
        }
    }
}
