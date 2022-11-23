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
            
            if (
                    (   // move like a rook
                    (fromSpace.Latitude == toSpace.Latitude) ||
                    (fromSpace.Longitude == toSpace.Longitude)
                    )
                    ||
                    (   // move like a bishop
                    (fromSpace.Latitude + 1 == toSpace.Latitude && fromSpace.Longitude + 1 == toSpace.Longitude) ||
                    (fromSpace.Latitude - 1 == toSpace.Latitude && fromSpace.Longitude + 1 == toSpace.Longitude)
                    )
                )
            {
                return true;
            }
            return false;
        }
    }
}
