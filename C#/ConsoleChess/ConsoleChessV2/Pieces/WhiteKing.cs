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
            spacesThisPieceCanMoveTo?.Clear();
            spacesThisPieceCanMoveTo?.Add(toSpace);

            // fix out of bounds issues
            //spacesThisPieceCanMoveTo?.Add(ChessBoard.Spaces![fromSpace.Row + 1][fromSpace.Column + 1]);
            //spacesThisPieceCanMoveTo?.Add(ChessBoard.Spaces![fromSpace.Row + 1][fromSpace.Column - 1]);
            //spacesThisPieceCanMoveTo?.Add(ChessBoard.Spaces![fromSpace.Row - 1][fromSpace.Column + 1]);
            //spacesThisPieceCanMoveTo?.Add(ChessBoard.Spaces![fromSpace.Row - 1][fromSpace.Column - 1]);
            //spacesThisPieceCanMoveTo?.Add(ChessBoard.Spaces![fromSpace.Row + 1][fromSpace.Column]);
            //spacesThisPieceCanMoveTo?.Add(ChessBoard.Spaces![fromSpace.Row - 1][fromSpace.Column]);
            //spacesThisPieceCanMoveTo?.Add(ChessBoard.Spaces![fromSpace.Row][fromSpace.Column + 1]);
            //spacesThisPieceCanMoveTo?.Add(ChessBoard.Spaces![fromSpace.Row][fromSpace.Column - 1]);
        }
    }
}
