namespace ConsoleChessV2.Pieces
{
    internal class BlackPawn : Piece
    {
        public BlackPawn()
        {
            Name = "[p]";
            PointValue = 1;
            BelongsTo = Player.Black;
        }
        public override bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (fromSpace == toSpace)
            {
                return false;
            }
            if (fromSpace.Column == toSpace.Column && 
                fromSpace.Row - 1 == toSpace.Row ||
                //(HasMoved == false &&
                 fromSpace.Column == toSpace.Column && 
                 fromSpace.Row - 2 == toSpace.Row)//)
            {
                return true;
            }
            return false;
        }

        public override bool CanLegallyTryToCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if ((fromSpace.Column + 1 == toSpace.Column && 
                fromSpace.Row - 1 == toSpace.Row) 
                ||
                (fromSpace.Column - 1 == toSpace.Column && 
                fromSpace.Row - 1 == toSpace.Row))
            {
                return true;
            }
            return false;
        }
        public override void CreateListOfPiecesToInspect(Space fromSpace, Space toSpace)
        {
            spacesToMoveToReview?.Clear();
            if (toSpace.Column == fromSpace.Column)
            {
                // moving up
                for (int row = fromSpace.Row - 1; row >= toSpace.Row; row--)
                {
                    spacesToMoveToReview!.Add(ChessBoard.Spaces![fromSpace.Column][row]);
                }
            }
            else if (fromSpace.Column + 1 == toSpace.Column && 
                     fromSpace.Row - 1 == toSpace.Row)
            {
                // attacking up and left
                spacesToCaptureReview!.Add(ChessBoard.Spaces![toSpace.Column][toSpace.Row]);
            }
            else if (fromSpace.Column - 1 == toSpace.Column && 
                     fromSpace.Row - 1 == toSpace.Row)
            {
                // attacking up and right
                spacesToCaptureReview!.Add(ChessBoard.Spaces![toSpace.Column][toSpace.Row]);
            }
        }

        public override bool CanCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // we already know move selection is legal and piece is not blocked
            // if column or row is different, try to capture
            if (fromSpace.Column != toSpace.Column &&
                toSpace.Piece?.BelongsTo != null &&
                fromSpace.Piece?.BelongsTo != toSpace.Piece?.BelongsTo)
            {
                // pawn can capture
                return true;
            }
            return false;
        }
    }
}
