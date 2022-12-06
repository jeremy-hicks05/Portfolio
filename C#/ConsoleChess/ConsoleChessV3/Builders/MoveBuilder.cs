using ConsoleChessV3.Moves;
using ConsoleChessV3.Pieces;
using ConsoleChessV3.Pieces.White;
using ConsoleChessV3.SuperClasses;

namespace ConsoleChessV3.Builders
{
    internal static class MoveBuilder
    {
        public static ChessMove? Build(Space fromSpace, Space toSpace)
        {
            // TODO: finish EnPassant detection logic
            if (fromSpace.Piece is not null)
            {
                if (fromSpace.Piece is Pawn)
                {
                    return new EnPassant(fromSpace, toSpace);
                }
                // castle
                else if (fromSpace.Piece is King &&
                    Math.Abs(fromSpace.Column - toSpace.Column) == 2)
                {
                    return new Castle(fromSpace, toSpace);
                }
                // capture
                else if (toSpace.IsOccupied())
                {
                    return new Capture(fromSpace, toSpace);
                }
                // standard move
                else
                {
                    return new Move(fromSpace, toSpace);
                }
            }
            return null;
        }
    }
}
