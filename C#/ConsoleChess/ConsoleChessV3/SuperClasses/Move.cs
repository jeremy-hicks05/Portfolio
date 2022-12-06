using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleChessV3.Moves;

namespace ConsoleChessV3.SuperClasses
{
    internal class Move : ChessMove
    {
        public Move(Space initialSpace, Space targetSpace)
        {
            initialSpace.Piece.CanLegallyTryToMoveFromSpaceToSpace();
        }
    }
}
