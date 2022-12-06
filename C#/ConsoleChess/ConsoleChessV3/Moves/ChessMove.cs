namespace ConsoleChessV3.Moves
{
    using ConsoleChessV3.Interfaces;
    using ConsoleChessV3.Pieces;
    internal class ChessMove : IChessMove
    {
        public Space StartingSpace;
        public Space TargetSpace;

        public Piece StartingPiece;
        public Piece TargetPiece;
    }
}
