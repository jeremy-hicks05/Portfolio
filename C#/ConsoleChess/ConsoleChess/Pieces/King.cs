using ConsoleChess.Interfaces;

namespace ConsoleChess.Pieces
{
    internal class King : Piece
    {
        public King(string name) : base(name)
        {

        }

        public override void MoveTo(Space spaceMovedTo)
        {
            // move like a king
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        //public override Space[] GetSpacesAvaiableToMoveTo()
        //{
        //    Space[] spaces = new Space[8];
        //    spaces[0] = this.
        //}

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
