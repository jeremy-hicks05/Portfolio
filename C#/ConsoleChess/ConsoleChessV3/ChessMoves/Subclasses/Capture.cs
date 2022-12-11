namespace ConsoleChessV3.ChessMoves.Subclasses
{
    using ConsoleChessV3.ChessMoves;

    /// <summary>
    /// Represents the occurrence of a piece being captured in chess.  
    /// It holds the starting, ending, captured, and otherwise affected pieces
    /// when it comes to a capture.  Is reversed when TakeBack function is called.
    /// </summary>
    internal class Capture : ChessMove
    {
        public Capture(Space startingSpace, Space endingSpace) : base(startingSpace, endingSpace)
        {

            StartingSpace = startingSpace;
            StartingPiece = startingSpace.Piece!;
            StartingPieceHasMoved = StartingPiece.GetHasMoved();

            TargetSpace = endingSpace;
            if (TargetSpace.Piece is not null)
            {
                TargetPiece = endingSpace.Piece!;
                TargetPieceHasMoved = TargetPiece.GetHasMoved();
            }

            RestoreSpace = endingSpace;
            if (RestoreSpace.Piece is not null)
            {
                RestorePiece = endingSpace.Piece;
                RestorePieceHasMoved = RestorePiece != null && RestorePiece.GetHasMoved();
            }
        }
        public override void Perform()
        {
            if (StartingSpace is not null && StartingSpace.Piece is not null)
            {
                StartingSpace.Piece.Capture(StartingSpace, TargetSpace);
            }
        }

        public override void Reverse()
        {
            TargetSpace.Clear();
            if (RestoreSpace is not null)
            {
                RestoreSpace.Piece = RestorePiece;
            }
            if (RestoreSpace is not null && RestoreSpace.Piece is not null)
            {
                RestoreSpace.Piece.SetHasMoved(RestorePieceHasMoved);
            }

            StartingSpace.Piece = StartingPiece;
            StartingPiece.SetHasMoved(StartingPieceHasMoved);
        }

        public override bool IsValidChessMove()
        {
            if (StartingSpace is not null && StartingSpace.Piece is not null)
            {
                if (StartingSpace.Piece.CanLegallyTryToCaptureFromSpaceToSpace(StartingSpace, TargetSpace))
                {
                    if (StartingSpace.Piece.TryCapture(StartingSpace, TargetSpace))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
