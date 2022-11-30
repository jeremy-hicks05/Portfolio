namespace ConsoleChessV2.Pieces
{
    internal class WhiteKing : Piece
    {
        public WhiteKing()
        {
            Name = "[K]";
            PointValue = 99;
            BelongsTo = Player.White;
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
            spacesToMoveToReview?.Add(toSpace);

            // fix out of bounds issues
            //spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row + 1][fromSpace.Column + 1]);
            //spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row + 1][fromSpace.Column - 1]);
            //spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row - 1][fromSpace.Column + 1]);
            //spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row - 1][fromSpace.Column - 1]);
            //spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row + 1][fromSpace.Column]);
            //spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row - 1][fromSpace.Column]);
            //spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row][fromSpace.Column + 1]);
            //spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Row][fromSpace.Column - 1]);
        }
    }
}
