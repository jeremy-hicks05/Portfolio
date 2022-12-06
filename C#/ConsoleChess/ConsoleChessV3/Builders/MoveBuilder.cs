using ConsoleChessV3.Moves;
using ConsoleChessV3.Pieces;
using ConsoleChessV3.SuperClasses;

namespace ConsoleChessV3.Builders
{
    internal static class MoveBuilder
    {
        public static ChessMove Build(Space fromSpace, Space toSpace)
        {
            // move to empty space
            if(toSpace.IsEmpty())
            {
                return new Move(fromSpace, toSpace);
            }
            // capture
            else if(toSpace.IsOccupied())
            {
                return new Capture(fromSpace, toSpace);
            }
            // Castle
            else if(fromSpace.Piece.GetType() == typeof(King) && 
                Math.Abs(fromSpace.Column - toSpace.Column) == 2)
            {
                return new Castle(fromSpace, toSpace);
            }
            else if(fromSpace.Piece.GetType() == typeof(Pawn))
            {
                Console.WriteLine("Pawn move!");
            }

            // set up logic for determining if it is EnPassant
            return null;
        }
    }
}
