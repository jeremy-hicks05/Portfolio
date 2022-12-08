namespace ConsoleChessV3.Pieces.Black
{
    internal class BlackPawn : Pawn
    {
        public BlackPawn()
        {
            Name = "p";
            BelongsTo = Enums.Player.Black;
        }

        public override void BuildListOfSpacesToInspect(Space fromSpace, Space toSpace)
        {
            SpacesToReview.Clear();
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

        public override bool CanLegallyTryToCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            //capture up and left or up and right
            return (fromSpace.Column - 1 == toSpace.Column &&
                fromSpace.Row - 1 == toSpace.Row)
                ||
                (fromSpace.Column + 1 == toSpace.Column &&
                fromSpace.Row - 1 == toSpace.Row);
        }
    }
}
