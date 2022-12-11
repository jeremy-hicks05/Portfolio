using ConsoleChessV3.Pieces.Black;
using ConsoleChessV3.Pieces.Subclasses;

namespace ConsoleChessV3.Pieces.White
{
    internal class WhitePawn : Pawn
    {
        public WhitePawn()
        {
            Name = "P";
        }

        public override void BuildListOfSpacesToInspect(Space fromSpace, Space toSpace)
        {
            SpacesToReview.Clear();
            if (toSpace.Column == fromSpace.Column)
            {
                for (int row = fromSpace.Row + 1; row <= toSpace.Row; row++)
                {
                    SpacesToReview!.Add(ChessBoard.Spaces![fromSpace.Column][row]);
                }
            }
            else if (fromSpace.Column + 1 == toSpace.Column &&
                     fromSpace.Row + 1 == toSpace.Row)
            {
                // attacking up and right
                SpacesToReview!.Add(ChessBoard.Spaces![toSpace.Column][toSpace.Row]);
            }
            else if (fromSpace.Column - 1 == toSpace.Column &&
                     fromSpace.Row + 1 == toSpace.Row)
            {
                // attacking up and left
                SpacesToReview!.Add(ChessBoard.Spaces![toSpace.Column][toSpace.Row]);
            }
        }

        public override bool CanLegallyTryToCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            BlackPawn? tempBlackPawn;
            bool tempBlackPawnHasJustMoveTwo = false;
            if (ChessBoard.Spaces is not null)
            {
                if (fromSpace.Column - 1 >= 0 && ChessBoard.Spaces[fromSpace.Column - 1][fromSpace.Row].Piece is BlackPawn)
                {
                    tempBlackPawn = ChessBoard.Spaces[fromSpace.Column - 1][fromSpace.Row].Piece as BlackPawn;
                    tempBlackPawnHasJustMoveTwo = tempBlackPawn == null ? false : tempBlackPawn.HasJustMovedTwo;
                }
                else if (fromSpace.Column + 1 <= 7 && ChessBoard.Spaces[fromSpace.Column + 1][fromSpace.Row].Piece is BlackPawn)
                {
                    tempBlackPawn = ChessBoard.Spaces[fromSpace.Column + 1][fromSpace.Row].Piece as BlackPawn;
                    tempBlackPawnHasJustMoveTwo = tempBlackPawn == null ? false : tempBlackPawn.HasJustMovedTwo;
                }
            }
            //capture up and left or up and right
            return ((fromSpace.Column - 1 == toSpace.Column &&
                fromSpace.Row + 1 == toSpace.Row) ||
                (fromSpace.Column + 1 == toSpace.Column &&
                fromSpace.Row + 1 == toSpace.Row)) &&
                toSpace.IsOccupied()
                ||
                (((fromSpace.Column - 1 == toSpace.Column &&
                fromSpace.Row + 1 == toSpace.Row) ||
                (fromSpace.Column + 1 == toSpace.Column &&
                fromSpace.Row + 1 == toSpace.Row)) &&
                tempBlackPawnHasJustMoveTwo);
        }
    }
}
