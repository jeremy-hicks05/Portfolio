namespace ConsoleChessV3.Moves
{
    using ConsoleChessV3.SuperClasses;

    internal class EnPassant : ChessMove
    {
        public EnPassant(Space startingSpace, Space endingSpace) : base(startingSpace, endingSpace)
        {
            StartingSpace = startingSpace;
            TargetSpace = endingSpace;

            //CapturedSpace = piece to the left/right of pawn
            if (ChessBoard.Spaces is not null)
            {
                RestoreSpace =
                    StartingPiece.GetBelongsTo() == Enums.Player.White ?
                    ChessBoard.Spaces[TargetSpace.Column][TargetSpace.Row - 1] : // If WhitePawn
                    ChessBoard.Spaces[TargetSpace.Column][TargetSpace.Row + 1];  // If BlackPawn
            }
            RestorePiece = RestoreSpace.Piece;
        }

        public override void Perform()
        {
            if (StartingPiece.CanLegallyTryToCaptureFromSpaceToSpace(StartingSpace, TargetSpace))
            {
                //TODO: Insert code to perform an EnPassant capture
                TargetSpace.Piece = StartingPiece;
                StartingSpace.Clear();
                RestorePiece = RestoreSpace.Piece;
                RestoreSpace.Clear();
            }
        }

        public override bool IsValidChessMove()
        {
            if (StartingSpace is not null && StartingSpace.Piece is not null)
            {
                if (StartingSpace.Piece.CanLegallyTryToCaptureFromSpaceToSpace(StartingSpace, TargetSpace))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
