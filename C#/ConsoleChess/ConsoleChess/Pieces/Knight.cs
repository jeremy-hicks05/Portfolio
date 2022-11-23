using ConsoleChess.Interfaces;

namespace ConsoleChess.Pieces
{
    internal class Knight : Piece
    {
        public Knight(string name) : base(name)
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
            // if one of the differences is 1, and one of the differences is 2...
            if((Math.Abs(fromSpace.Latitude - toSpace.Latitude) == 1 && Math.Abs(fromSpace.Longitude - toSpace.Longitude) == 2) ||
                (Math.Abs(fromSpace.Longitude - toSpace.Longitude) == 1 && Math.Abs(fromSpace.Latitude - toSpace.Latitude) == 2))
            {
                return true;
            }
            if (fromSpace.Longitude + 1 == toSpace.Longitude && fromSpace.Latitude - 2 == toSpace.Latitude)
            {
                return true;
            }
            return false;
        }
    }
}
