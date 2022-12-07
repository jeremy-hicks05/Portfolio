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
    }
}
