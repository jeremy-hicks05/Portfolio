namespace ConsoleChessV3.SuperClasses
{
    using ConsoleChessV3.Interfaces;

    internal class ChessMove : IChessMove
    {
        public Space StartingSpace;
        public Space TargetSpace;

        public IPiece StartingPiece;
        public IPiece TargetPiece;

        public Space CapturedSpace;
        public IPiece? CapturedPiece;

        public ChessMove(Space startingSpace, Space endingSpace)
        {
            StartingSpace = startingSpace;
            TargetSpace = endingSpace;

            StartingPiece = startingSpace.Piece!;
            TargetPiece = endingSpace.Piece!;

            CapturedSpace = startingSpace;
            CapturedPiece = startingSpace.Piece;
        }

        public virtual void Perform()
        {
            //if (TargetSpace.Piece.CanLegallyTryToMoveFromSpaceToSpace(StartingSpace, TargetSpace))
            //{
            //    TargetSpace.Piece = StartingSpace.Piece;
            //    AffectedSpace?.Clear();
            //}
        }
    }
}
