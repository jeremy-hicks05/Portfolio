namespace ConsoleChessV2.Pieces
{
    internal class BlackKing : Piece
    {
        public BlackKing()
        {
            Name = "[k]";
            PointValue = 99;
            BelongsTo = Player.Black;
        }

        public override bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (fromSpace == toSpace)
            {
                return false;
            }
            if (Math.Abs(fromSpace.Column - toSpace.Column) <= 1 && Math.Abs(fromSpace.Row - toSpace.Row) <= 1)
            {
                return true;
            }
            return false;
        }
        public override void CreateListOfPiecesToInspect(Space fromSpace, Space toSpace)
        {
            spacesToMoveToReview?.Clear();

            // fix out of bounds issues
            if (fromSpace.Column - 1 >= 0)
            {
                spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row][fromSpace.Column - 1]);
            }
            if (fromSpace.Row - 1 >= 0)
            {
                spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row - 1][fromSpace.Column]);
            }
            if (fromSpace.Row + 1 <= 7)
            {
                spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row + 1][fromSpace.Column]);
            }
            if (fromSpace.Column + 1 <= 7)
            {
                spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row][fromSpace.Column + 1]);
            }
            if (fromSpace.Row + 1 <= 7 && fromSpace.Column + 1 <= 7)
            {
                spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row + 1][fromSpace.Column + 1]);
            }
            if (fromSpace.Row + 1 <= 7 && fromSpace.Column - 1 >= 0)
            {
                spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row + 1][fromSpace.Column - 1]);
            }
            if (fromSpace.Row - 1 >= 0 && fromSpace.Column + 1 <= 7)
            {
                spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row - 1][fromSpace.Column + 1]);
            }

            if (fromSpace.Column - 1 >= 0 && fromSpace.Row - 1 >= 0)
            {
                spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row - 1][fromSpace.Column - 1]);
            }
        }
    }
}
