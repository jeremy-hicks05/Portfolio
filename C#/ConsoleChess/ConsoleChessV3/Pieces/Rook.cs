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
        public override bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return fromSpace.Column - toSpace.Column == 0 ||
                    fromSpace.Row - toSpace.Row == 0;
        }

        public override bool CanLegallyTryToCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return CanLegallyTryToMoveFromSpaceToSpace(fromSpace, toSpace)
                && fromSpace.Piece.GetBelongsTo() != toSpace.Piece.GetBelongsTo();
        }
    }
}
