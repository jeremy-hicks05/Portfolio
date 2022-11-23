using ConsoleChess.Interfaces;
using ConsoleChess.Players;

namespace ConsoleChess.Pieces
{
    internal class Knight : Piece
    {
        public Knight(string name, Player belongsTo) : base(name, belongsTo)
        {

        }

        public override void MoveTo(Space spaceMovedTo)
        {
            // move like a knight
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // if one of the differences is 1, and one of the differences is 2... clever!
            if((Math.Abs(fromSpace.Latitude - toSpace.Latitude) == 1 && Math.Abs(fromSpace.Longitude - toSpace.Longitude) == 2) ||
                (Math.Abs(fromSpace.Longitude - toSpace.Longitude) == 1 && Math.Abs(fromSpace.Latitude - toSpace.Latitude) == 2))
            {
                return true;
            }
            return false;
        }
    }
}
