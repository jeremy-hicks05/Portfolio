using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    /* ToDo
     * 1. Stop pawns from jumping over pieces during first move
     * 2. Allow en passant rule
     */
    internal class Pawn : Piece
    {
        //public bool movedTwoLastTurn;
        //public bool hasMoved;
        public Pawn(string name, Player belongsTo) : base(name, belongsTo)
        {
            hasMoved = false;
        }

        public override bool CanAttackSpace(Space fromSpace, Space toSpace)
        {
            // done

            // if this is a white piece - let it attack up and right and up and left
            //if (fromSpace.X > toSpace.X)
            //{
            //    if (belongsToPlayer == Player.White && (fromSpace.X == toSpace.X + 1 && fromSpace.Y == toSpace.Y - 1))
            //    {
            //        return true;
            //    }

            //    if (belongsToPlayer == Player.White && (fromSpace.X == toSpace.X + 1 && fromSpace.Y == toSpace.Y + 1))
            //    {
            //        return true;
            //    }
            //}
            //else if (fromSpace.X < toSpace.X)
            //{
            //    if (belongsToPlayer == Player.Black && (fromSpace.X == toSpace.X - 1 && fromSpace.Y == toSpace.Y + 1))
            //    {
            //        return true;
            //    }

            //    if (belongsToPlayer == Player.Black && (fromSpace.X == toSpace.X - 1 && fromSpace.Y == toSpace.Y - 1))
            //    {
            //        return true;
            //    }
            //}
            return false;
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (fromSpace == toSpace)
            {
                return false;
            }
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
                if (toSpace.Y == fromSpace.Y && Math.Abs(fromSpace.X - toSpace.X) < 3)
                {
                    // if player is white and space next to pawn (above) is occupied, return false
                    if(fromSpace.Piece.belongsToPlayer == Player.White && Board.spaces[fromSpace.X - 1][fromSpace.Y].Piece.belongsToPlayer != Player.None)
                    {
                        return false;
                    }
                    // if player is black and space next to pawn (above) is occupied, return false
                    else if (fromSpace.Piece.belongsToPlayer == Player.Black && Board.spaces[fromSpace.X + 1][fromSpace.Y].Piece.belongsToPlayer != Player.None)
                    {
                        return false;
                    }
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
