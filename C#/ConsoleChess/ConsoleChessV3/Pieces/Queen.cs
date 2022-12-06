using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleChessV3.SuperClasses;

namespace ConsoleChessV3.Pieces
{
    internal class Queen : Piece
    {
        public override bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return true;
        }
    }
}
