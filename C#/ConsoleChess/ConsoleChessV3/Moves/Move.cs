using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleChessV3.SuperClasses;

namespace ConsoleChessV3.Moves
{
    internal class Move : ChessMove
    {
        public Move(Space initialSpace, Space targetSpace) : base(initialSpace, targetSpace)
        {

        }

        public override void Perform()
        {
            if (StartingSpace is not null && StartingSpace.Piece is not null)
            {
                StartingSpace.Piece.Move(StartingSpace, TargetSpace);
            }
        }

        public override void Reverse()
        {
            TargetSpace.Clear();
            StartingSpace.Piece = StartingPiece;
            StartingPiece.SetHasMoved(StartingPieceHasMoved);
        }
    }
}
