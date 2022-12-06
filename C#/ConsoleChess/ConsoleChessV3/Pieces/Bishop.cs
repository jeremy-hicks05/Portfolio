using ConsoleChessV3.Interfaces;
using ConsoleChessV3.SuperClasses;

namespace ConsoleChessV3.Pieces
{
    internal class Bishop : Piece
    {
        public override bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (fromSpace.Column != toSpace.Column && fromSpace.Row != toSpace.Row) // prevent dividing by zero
            {
                if ((float)Math.Abs(fromSpace.Column - toSpace.Column) / (float)Math.Abs(fromSpace.Row - toSpace.Row) == 1)
                {
                    // slope meets criteria
                    return true;
                }
            }
            // slope does not meet criteria
            return false;
        }

        public override void Capture(Space fromSpace, Space toSpace)
        {
            toSpace.Piece = fromSpace.Piece;
            fromSpace.Clear();
        }
    }
}
