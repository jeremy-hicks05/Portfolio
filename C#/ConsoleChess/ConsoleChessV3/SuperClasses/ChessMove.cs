namespace ConsoleChessV3.SuperClasses
{
    using ConsoleChessV3.Interfaces;

    internal class ChessMove : IChessMove
    {
        public Space StartingSpace = new();
        public Space TargetSpace = new();

        public IPiece StartingPiece;
        public IPiece TargetPiece;

        public Space AffectedSpace = new();
        public IPiece AffectedPiece;

        public ChessMove(Space startingSpace, Space endingSpace, Space affectedSpace)
        {
            StartingSpace = startingSpace;
            TargetSpace = endingSpace;

            StartingPiece = startingSpace.Piece!;
            TargetPiece = endingSpace.Piece!;

            AffectedSpace = affectedSpace;
            AffectedPiece = affectedSpace.Piece;
        }
    }
}
