using ConsoleChessV5.ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV5.Moves
{
    internal class EnPassant : Capture
    {
        public EnPassant(Space firstSpace, Space secondSpace) : base(firstSpace, secondSpace) 
        { 
            
        }
    }
}
