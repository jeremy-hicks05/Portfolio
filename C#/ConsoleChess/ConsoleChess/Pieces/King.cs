using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    internal class King : Piece
    {
        public King(string name, Player belongsTo) : base(name, belongsTo)
        {

        }

        public override void MoveTo(Space spaceMovedTo)
        {
            // move like a king
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (toSpace.X - fromSpace.X > 1 || toSpace.Y - fromSpace.Y > 1)
            {
                return false;
            }
            else if (toSpace.Piece.belongsToPlayer != Player.None)
            {
                return false;
            }

            return true;
        }
    }
}
