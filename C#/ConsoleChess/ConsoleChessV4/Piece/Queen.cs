using ConsoleChessV4.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.Piece
{
    internal class Queen : Abstract.Piece
    {
        public Queen() 
        {
            PieceIcon = 'Q';
        }
    }
}
