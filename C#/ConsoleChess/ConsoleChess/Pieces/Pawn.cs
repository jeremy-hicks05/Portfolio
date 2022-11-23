using ConsoleChess.Interfaces;
using ConsoleChess.Players;

namespace ConsoleChess.Pieces
{
    internal class Pawn : Piece
    {
        public bool hasMoved;
        public Pawn(string name, Player belongsTo) : base(name, belongsTo)
        {
            hasMoved = false;
        }

        public override void MoveTo(Space spaceMovedTo)
        {
            // move like a pawn
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (!hasMoved)
            {
                if (toSpace.Longitude == fromSpace.Longitude && fromSpace.Latitude - toSpace.Latitude < 3)
                {
                    hasMoved = true;
                    return true;
                }
            }
            if (fromSpace.Longitude == toSpace.Longitude && Math.Abs(fromSpace.Latitude - toSpace.Latitude) < 2)
            {
                hasMoved = true;
                return true;
            }
            return false;
        }
    }
}
