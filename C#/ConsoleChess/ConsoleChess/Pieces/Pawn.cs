using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    /* ToDo
     * 1. Stop pawns from moving backwards
     * 2. Allow en passant rule
     */
    internal class Pawn : Piece
    {
        public bool hasMoved;
        public Pawn(string name, Player belongsTo) : base(name, belongsTo)
        {
            hasMoved = false;
        }

        //public override void MoveTo(Space spaceMovedTo)
        //{
        //    // move like a pawn
        //    spaceMovedTo.Piece.Name = Name;
        //    Name = "[ ]";
        //}

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

            // if piece is white and attacking a space up and left
            if((belongsToPlayer == Player.White) && 
                (toSpace.X == fromSpace.X - 1 && toSpace.Y == fromSpace.Y - 1) &&
                (toSpace.Piece.belongsToPlayer != Player.None && toSpace.Piece.belongsToPlayer != belongsToPlayer))
            {
                // capture piece
                return true;
            }

            // if piece is white and attacking a space up and right
            if ((belongsToPlayer == Player.White) &&
                (toSpace.X == fromSpace.X - 1 && toSpace.Y == fromSpace.Y + 1) &&
                (toSpace.Piece.belongsToPlayer != Player.None && toSpace.Piece.belongsToPlayer != belongsToPlayer))
            {
                // capture piece
                return true;
            }

            // if piece is black and attacking a space down and right
            if ((belongsToPlayer == Player.Black) &&
                (toSpace.X == fromSpace.X + 1 && toSpace.Y == fromSpace.Y + 1) &&
                (toSpace.Piece.belongsToPlayer != Player.None && toSpace.Piece.belongsToPlayer != belongsToPlayer))
            {
                // capture piece
                return true;
            }

            // if piece is black and attacking a space down and left
            if ((belongsToPlayer == Player.Black) &&
                (toSpace.X == fromSpace.X + 1 && toSpace.Y == fromSpace.Y - 1) &&
                (toSpace.Piece.belongsToPlayer != Player.None && toSpace.Piece.belongsToPlayer != belongsToPlayer))
            {
                // capture piece
                return true;
            }

            if (!hasMoved)
            {
                // added absolute value here - not sure why it wasn't there before
                if (toSpace.Y == fromSpace.Y && Math.Abs(fromSpace.X - toSpace.X) < 3)
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
