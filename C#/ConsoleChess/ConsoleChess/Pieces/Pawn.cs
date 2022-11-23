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
                if (toSpace.Y == fromSpace.Y && fromSpace.X - toSpace.X < 3)
                {
                    if (toSpace.Piece.belongsToPlayer == Player.None)
                    {
                        hasMoved = true;
                        return true;
                    }
                }
            }
            else if (fromSpace.Y == toSpace.Y && Math.Abs(fromSpace.X - toSpace.X) < 2)
            {
                if (toSpace.Piece.belongsToPlayer == Player.None)
                {
                    hasMoved = true;
                    return true;
                }
            }
            return false;
        }
    }
}
