using ConsoleChess.Interfaces;
using ConsoleChess.Players;

namespace ConsoleChess.Pieces
{
    /* ToDo
     * 1. Stop pawns from moving backwards
     * 
     */
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
            // if this is a white piece - don't allow it to move down
            if (belongsToPlayer == Player.White && toSpace.X > fromSpace.X)
            {
                return false;
            }

            // if this is a black piece - don't allow it to move up (Y-)
            if(belongsToPlayer == Player.Black && toSpace.X < fromSpace.X)
            {
                return false;
            }
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
