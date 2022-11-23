using ConsoleChess.Interfaces;
using System.Xml.Linq;

namespace ConsoleChess.Pieces
{
    internal class Bishop : Piece
    {
        public Bishop(string name) : base(name)
        {

        }

        public override void MoveTo(Space spaceMovedTo)
        {
            // move like a bishop
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if((fromSpace.Latitude + 1 == toSpace.Latitude && fromSpace.Longitude + 1 == toSpace.Longitude) ||
                (fromSpace.Latitude + 1 == toSpace.Latitude && fromSpace.Longitude - 1 == toSpace.Longitude) ||
                (fromSpace.Latitude - 1 == toSpace.Latitude && fromSpace.Longitude + 1 == toSpace.Longitude) ||
                (fromSpace.Latitude - 1 == toSpace.Latitude && fromSpace.Longitude - 1 == toSpace.Longitude))
            {
                return true;
            }
            return false;
        }
    }
}
