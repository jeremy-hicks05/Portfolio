using ConsoleChess.Interfaces;
using ConsoleChess.Players;

namespace ConsoleChess.Pieces
{
    internal class Queen : Piece
    {
        public Queen(string name, Player belongsTo) : base(name, belongsTo)
        {

        }

        public override void MoveTo(Space spaceMovedTo)
        {
            // move like a queen
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            
            if (
                    (   // move like a rook
                    (fromSpace.X == toSpace.X) ||
                    (fromSpace.Y == toSpace.Y)
                    )
                    ||
                    (   // move like a bishop
                    (fromSpace.X + 1 == toSpace.X && fromSpace.Y + 1 == toSpace.Y) ||
                    (fromSpace.X - 1 == toSpace.X && fromSpace.Y + 1 == toSpace.Y)
                    )
                )
            {
                return true;
            }
            return false;
        }
    }
}
