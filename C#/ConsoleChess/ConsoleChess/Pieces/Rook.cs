using ConsoleChess.Interfaces;

namespace ConsoleChess.Pieces
{
    internal class Rook : Piece
    {
        public Rook(string name) : base(name)
        {

        }

        public override void MoveTo(Space spaceMovedTo)
        {
            // move like a rook
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if(fromSpace.Latitude == toSpace.Latitude || fromSpace.Longitude == toSpace.Longitude)
            {
                return true;
            }
            return false;
        }
    }
}
