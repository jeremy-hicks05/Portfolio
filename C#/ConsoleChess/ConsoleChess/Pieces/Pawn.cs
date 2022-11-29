using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    internal class Pawn : Piece
    {
        public Pawn(string name, Player belongsTo) : base(name, belongsTo)
        {
            hasMoved = false;
        }

        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
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

            // if this is a black piece - don't allow it to move up
            if (belongsToPlayer == Player.Black && toSpace.X < fromSpace.X)
            {
                return false;
            }

            // if piece is white and attacking a space up and left
            if ((belongsToPlayer == Player.White) &&
                (toSpace.X == fromSpace.X - 1 && toSpace.Y == fromSpace.Y - 1))
            {
                return true;
            }

            // if piece is white and attacking a space up and right
            if ((belongsToPlayer == Player.White) &&
                (toSpace.X == fromSpace.X - 1 && toSpace.Y == fromSpace.Y + 1))
            {
                return true;
            }

            // if piece is black and attacking a space down and right
            if ((belongsToPlayer == Player.Black) &&
                (toSpace.X == fromSpace.X + 1 && toSpace.Y == fromSpace.Y + 1))
            {
                return true;
            }

            // if piece is black and attacking a space down and left
            if ((belongsToPlayer == Player.Black) &&
                (toSpace.X == fromSpace.X + 1 && toSpace.Y == fromSpace.Y - 1))
            {
                return true;
            }

            // allow 2 spaces on first move
            if (!hasMoved)
            {
                // TODO: refuse attempted move for White if moving down, Black refused if pawn moving up
                if (toSpace.Y == fromSpace.Y && Math.Abs(fromSpace.X - toSpace.X) < 3)
                {
                    return true;
                }
            }
            // if pawn has moved - restrict to one space
            else if (fromSpace.Y == toSpace.Y &&
                Math.Abs(fromSpace.X - toSpace.X) < 2)
            {
                return true;
            }

            // fallout false
            return false;
        }

        public override bool HasPiecesBlockingMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // add capture check here?
            if(fromSpace.Y != toSpace.Y)
            {
                return !CanTryToCapture(fromSpace, toSpace);
            }

            // single space move
            if(toSpace.Piece.belongsToPlayer != Player.None)
            {
                // is blocked
                return true;
            }

            // double space move
            if (fromSpace.Piece.belongsToPlayer ==
                        Player.White &&
                        Board.spaces[fromSpace.X - 1][fromSpace.Y]
                        .Piece.belongsToPlayer != Player.None)
            {
                // is blocked
                return true;
            }
            // if player is black and space next to pawn (above) is
            // occupied, return false
            else if (fromSpace.Piece.belongsToPlayer ==
                Player.Black &&
                Board.spaces[fromSpace.X + 1][fromSpace.Y]
                .Piece.belongsToPlayer != Player.None)
            {
                // is blocked
                return true;
            }
            else
            {
                // is not blocked
                return false;
            }

            //return true;
        }

        public override bool CanTryToCapture(Space fromSpace, Space toSpace)
        {

            // check for en passant here?

            // if this is a white piece - let it attack up and right and up and left
            if (fromSpace.X > toSpace.X)
            {
                if (belongsToPlayer == Player.White &&
                    (fromSpace.X == toSpace.X + 1 &&
                    fromSpace.Y == toSpace.Y - 1))
                {
                    return true;
                }

                if (belongsToPlayer == Player.White &&
                    (fromSpace.X == toSpace.X + 1 &&
                    fromSpace.Y == toSpace.Y + 1))
                {
                    return true;
                }
            }
            else if (fromSpace.X < toSpace.X)
            {
                if (belongsToPlayer == Player.Black &&
                    (fromSpace.X == toSpace.X - 1 &&
                    fromSpace.Y == toSpace.Y + 1))
                {
                    return true;
                }

                if (belongsToPlayer == Player.Black &&
                    (fromSpace.X == toSpace.X - 1 &&
                    fromSpace.Y == toSpace.Y - 1))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (belongsToPlayer == toSpace.Piece.belongsToPlayer)
            {
                return false;
            }
            // if this is a white piece - don't allow it to move down
            if (belongsToPlayer == Player.White && toSpace.X > fromSpace.X)
            {
                return false;
            }

            // if this is a black piece - don't allow it to move up
            if (belongsToPlayer == Player.Black && toSpace.X < fromSpace.X)
            {
                return false;
            }

            // if piece is white and attacking a space up and left
            if ((belongsToPlayer == Player.White) &&
                (toSpace.X == fromSpace.X - 1 && toSpace.Y == fromSpace.Y - 1) &&
                (toSpace.Piece.belongsToPlayer != Player.None))
            {
                // capture piece
                return true;
            }

            // if piece is white and attacking a space up and right
            if ((belongsToPlayer == Player.White) &&
                (toSpace.X == fromSpace.X - 1 && toSpace.Y == fromSpace.Y + 1) &&
                (toSpace.Piece.belongsToPlayer != Player.None))
            {
                // capture piece
                return true;
            }

            // if piece is black and attacking a space down and right
            if ((belongsToPlayer == Player.Black) &&
                (toSpace.X == fromSpace.X + 1 && toSpace.Y == fromSpace.Y + 1) &&
                (toSpace.Piece.belongsToPlayer != Player.None))
            {
                // capture piece
                return true;
            }

            // if piece is black and attacking a space down and left
            if ((belongsToPlayer == Player.Black) &&
                (toSpace.X == fromSpace.X + 1 && toSpace.Y == fromSpace.Y - 1) &&
                (toSpace.Piece.belongsToPlayer != Player.None))
            {
                // capture piece
                return true;
            }

            // allow 2 spaces on first move
            if (!hasMoved)
            {
                if (toSpace.Y == fromSpace.Y && Math.Abs(fromSpace.X - toSpace.X) < 3)
                {
                    // if player is white and space next to pawn (above) is
                    // occupied, return false
                    if (fromSpace.Piece.belongsToPlayer ==
                        Player.White &&
                        Board.spaces[fromSpace.X - 1][fromSpace.Y]
                        .Piece.belongsToPlayer != Player.None)
                    {
                        return false;
                    }
                    // if player is black and space next to pawn (above) is
                    // occupied, return false
                    else if (fromSpace.Piece.belongsToPlayer ==
                        Player.Black &&
                        Board.spaces[fromSpace.X + 1][fromSpace.Y]
                        .Piece.belongsToPlayer != Player.None)
                    {
                        return false;
                    }
                    if (toSpace.Piece.belongsToPlayer == Player.None)
                    {
                        //hasMoved = true;
                        return true;
                    }
                }
            }
            // if pawn has moved - restrict to one space
            else if (fromSpace.Y == toSpace.Y &&
                Math.Abs(fromSpace.X - toSpace.X) < 2)
            {
                if (toSpace.Piece.belongsToPlayer == Player.None)
                {
                    //hasMoved = true;
                    return true;
                }
            }

            // if no criteria is met, refuse pawn move
            return false;
        }
    }
}
