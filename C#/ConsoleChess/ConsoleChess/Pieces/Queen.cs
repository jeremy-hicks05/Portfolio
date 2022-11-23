using ConsoleChess.Interfaces;

namespace ConsoleChess.Pieces
{
    internal class Queen : Piece
    {
        public Queen(string name) : base(name)
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
            if (toSpace.Latitude - fromSpace.Latitude > 1 || toSpace.Longitude - fromSpace.Longitude > 1)
            {
                return false;
            }
            return true;
        }
    }
}
