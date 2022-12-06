using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleChessV3.SuperClasses;

namespace ConsoleChessV3.Pieces
{
    internal class Rook : Piece
    {
        public override bool CanLegallyTryToMoveFromSpaceToSpace()
        {
            return true;
        }
    }
}
