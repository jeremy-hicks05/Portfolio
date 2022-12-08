namespace ConsoleChessV3.Moves
{
    using ConsoleChessV3.SuperClasses;

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
                RestorePieceHasMoved = RestorePiece.GetHasMoved();
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
            RestoreSpace.Piece = RestorePiece;
            if (RestoreSpace.Piece is not null)
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
