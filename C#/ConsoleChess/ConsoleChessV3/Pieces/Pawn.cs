using ConsoleChessV3.SuperClasses;

namespace ConsoleChessV3.Pieces
{
    internal class Pawn : Piece
    {
        public bool HasJustMovedTwo { get; set; }
        public override bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return (fromSpace.Piece is not null) &&
                    (toSpace.IsEmpty()) &&
                    ((fromSpace.Column == toSpace.Column &&
                        toSpace.Row - fromSpace.Row <= 1) 
                    ||
                    (!fromSpace.Piece.GetHasMoved() &&
                    fromSpace.Column == toSpace.Column &&
                        toSpace.Row - fromSpace.Row <= 2));
        }

        public override bool CanLegallyTryToCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            //TODO : stop pawn from capturing backwards - move to BlackPawn and WhitePawn
            return Math.Abs(toSpace.Column - fromSpace.Column) == 1 &&
                Math.Abs(toSpace.Row - fromSpace.Row) == 1;
        }

        public override void BuildListOfSpacesToInspect(Space fromSpace, Space toSpace)
        {
            SpacesToReview.Clear();
            //spacesToCaptureReview?.Clear();
            if (toSpace.Column == fromSpace.Column)
            {
                // moving down
                for (int row = fromSpace.Row - 1; row >= toSpace.Row; row--)
                {
                    SpacesToReview!.Add(ChessBoard.Spaces![fromSpace.Column][row]);
                }
            }
            else if (fromSpace.Column + 1 == toSpace.Column &&
                     fromSpace.Row - 1 == toSpace.Row)
            {
                // attacking down and right
                SpacesToReview!.Add(ChessBoard.Spaces![toSpace.Column][toSpace.Row]);
            }
            else if (fromSpace.Column - 1 == toSpace.Column &&
                     fromSpace.Row - 1 == toSpace.Row)
            {
                // attacking down and left
                SpacesToReview!.Add(ChessBoard.Spaces![toSpace.Column][toSpace.Row]);
            }
        }

        public override void Move(Space fromSpace, Space toSpace)
        {
            HasJustMovedTwo = false;
            if (fromSpace.Piece is not null && toSpace.IsEmpty())
            {
                fromSpace.Piece.SetHasMoved(true);
                if(Math.Abs(toSpace.Row - fromSpace.Row) == 2)
                {
                    HasJustMovedTwo = true;
                }
                toSpace.Piece = fromSpace.Piece;
                fromSpace.Clear();
            }
        }
    }
}
