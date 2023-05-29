using ConsoleChessV5.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV5.ChessBoard
{
    internal class Space
    {
        internal Abstract.Piece? Piece { get; set; }
        internal int Row { get; set; }
        internal int Column { get; set; }
        public Space()
        {

        }
    }
}
