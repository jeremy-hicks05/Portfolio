namespace ConsoleChessV3.SuperClasses
{
    using ConsoleChessV3.Interfaces;

    internal class ChessMove : IChessMove
    {
        public Space StartingSpace;
        public Space TargetSpace;

        public IPiece StartingPiece;
        public IPiece TargetPiece;

        public Space AffectedSpace;
        public IPiece? AffectedPiece;

        public ChessMove(Space startingSpace, Space endingSpace)
        {
            StartingSpace = startingSpace;
            TargetSpace = endingSpace;

            StartingPiece = startingSpace.Piece!;
            TargetPiece = endingSpace.Piece!;

            AffectedSpace = startingSpace;
            AffectedPiece = startingSpace.Piece;
        }

        public virtual void Perform()
        {
            TargetSpace.Piece = StartingSpace.Piece;
            AffectedSpace?.Clear();
        }
    }
}
