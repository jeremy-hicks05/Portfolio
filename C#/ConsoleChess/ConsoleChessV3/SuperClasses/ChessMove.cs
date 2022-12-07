namespace ConsoleChessV3.SuperClasses
{
    using ConsoleChessV3.Interfaces;

    internal class ChessMove : IChessMove
    {
        public Space StartingSpace;
        public Space TargetSpace;

        public IPiece StartingPiece;
        public IPiece TargetPiece;

        public Space RestoreSpace;
        public IPiece? RestorePiece;

        public ChessMove(Space startingSpace, Space endingSpace)
        {
            StartingSpace = startingSpace;
            StartingPiece = startingSpace.Piece!;

            TargetSpace = endingSpace;
            TargetPiece = endingSpace.Piece!;

            RestoreSpace = endingSpace;
            RestorePiece = endingSpace.Piece;
        }

        public virtual void Perform()
        {
            //if (TargetSpace.Piece.CanLegallyTryToMoveFromSpaceToSpace(StartingSpace, TargetSpace))
            //{
            //    TargetSpace.Piece = StartingSpace.Piece;
            //    AffectedSpace?.Clear();
            //}
        }

        public virtual bool IsValidChessMove()
        {
            if (StartingSpace is not null && StartingSpace.Piece is not null)
            {
                if (StartingSpace.Piece.CanLegallyTryToMoveFromSpaceToSpace(StartingSpace, TargetSpace))
                {
                    if (!StartingSpace.Piece.IsBlocked(StartingSpace, TargetSpace))
                    {
                        if (StartingSpace.Piece.TryMove(StartingSpace, TargetSpace))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
