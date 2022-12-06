using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleChessV3.SuperClasses;

namespace ConsoleChessV3.Pieces
{
    internal class King : Piece
    {
        public override bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return Math.Abs(fromSpace.Column - toSpace.Column) <= 1
                   && Math.Abs(fromSpace.Row - toSpace.Row) <= 1;
        }

        public override void Capture(Space fromSpace, Space toSpace)
        {
            base.Capture(fromSpace, toSpace);
        }
    }
}
