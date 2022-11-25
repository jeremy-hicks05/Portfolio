using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

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
            if((Math.Abs(fromSpace.X - toSpace.X) == 1 && Math.Abs(fromSpace.Y - toSpace.Y) == 2) ||
                (Math.Abs(fromSpace.Y - toSpace.Y) == 1 && Math.Abs(fromSpace.X - toSpace.X) == 2))
            {
                if(toSpace.Piece.belongsToPlayer == belongsToPlayer)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
