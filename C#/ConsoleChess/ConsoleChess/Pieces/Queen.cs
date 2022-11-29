using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    internal class Queen : Piece
    {
        public Queen(string name, Player belongsTo) : base(name, belongsTo)
        {

        }

        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // test whether attempted move follows the rules of chess
            if (fromSpace == toSpace)
            {
                return false;
            }
            // if move like bishop
            if ((float)(Math.Abs(fromSpace.X - toSpace.X)) / (float)(Math.Abs(fromSpace.Y - toSpace.Y)) == 1)
            {
                return true;
            }
            // if move like rook
            if (fromSpace.X == toSpace.X || fromSpace.Y == toSpace.Y)
            {
                return true;
            }
            // fallout false
            return false;
        }

        public override bool HasPiecesBlockingMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // MOVE AS BISHOP
            // if toSpace is down and right
            if (fromSpace.X < toSpace.X && fromSpace.Y < toSpace.Y)
            {
                for (int i = fromSpace.X + 1, k = fromSpace.Y + 1; i <= toSpace.X && k <= toSpace.Y; i++, k++)
                {
                    if (i == toSpace.X && k == toSpace.Y)
                    {
                        // is not blocked
                        return false;
                    }
                    if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                    {
                        // is blocked
                        return true;
                    }
                }
            }
            // if toSpace is down and left
            else if (fromSpace.X < toSpace.X && fromSpace.Y > toSpace.Y)
            {
                for (int i = fromSpace.X + 1, k = fromSpace.Y - 1; i <= toSpace.X && k >= toSpace.Y; i++, k--)
                {
                    if ((i == toSpace.X && k == toSpace.Y))
                    {
                        // is not blocked
                        return false;
                    }
                    if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                    {
                        // is blocked
                        return true;
                    }
                }
            }
            // if toSpace is up and right
            else if (fromSpace.X > toSpace.X && fromSpace.Y < toSpace.Y)
            {
                for (int i = fromSpace.X - 1, k = fromSpace.Y + 1; i >= toSpace.X && k <= toSpace.Y; i--, k++)
                {
                    if ((i == toSpace.X && k == toSpace.Y))
                    {
                        // is not blocked
                        return false;
                    }
                    if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                    {
                        // is blocked
                        return true;
                    }
                }
            }
            // if toSpace is up and left
            else if (fromSpace.X > toSpace.X && fromSpace.Y > toSpace.Y)
            {
                for (int i = fromSpace.X - 1, k = fromSpace.Y - 1; i >= toSpace.X && k >= toSpace.Y; i--, k--)
                {
                    if ((i == toSpace.X && k == toSpace.Y))
                    {
                        // is not blocked
                        return false;
                    }
                    if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                    {
                        // is blocked
                        return true;
                    }
                }
            }
            // MOVE AS ROOK
            // if space is above
            if (fromSpace.X > toSpace.X)
            {
                for (int i = fromSpace.X - 1; i >= toSpace.X; i--)
                {
                    if (i == toSpace.X)
                    {
                        // not blocked
                        return false;
                    }

                    if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer !=
                        Player.None)
                    {
                        // blocked
                        return true;
                    }
                }
            }
            else if (fromSpace.X < toSpace.X)
            {
                // if space is below
                for (int i = fromSpace.X + 1; i <= toSpace.X; i++)
                {
                    if (i == toSpace.X)
                    {
                        // not blocked
                        return false;
                    }

                    if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer !=
                        Player.None)
                    {
                        // blocked
                        return true;
                    }
                }
            }
            // if space is to the left
            else if (fromSpace.Y > toSpace.Y)
            {
                for (int i = fromSpace.Y - 1; i >= toSpace.Y; i--)
                {
                    if (i == toSpace.Y)
                    {
                        // not blocked
                        return false;
                    }

                    if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer !=
                        Player.None)
                    {
                        // blocked
                        return true;
                    }
                }
            }
            else if (fromSpace.Y < toSpace.Y)
            {
                // if space is to the right
                for (int i = fromSpace.Y + 1; i <= toSpace.Y; i++)
                {
                    if (i == toSpace.Y)
                    {
                        // not blocked
                        return false;
                    }

                    if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer !=
                        Player.None)
                    {
                        // blocked
                        return true;
                    }
                }
            }
            // fallout true
            return true;
        }

        public override bool CanTryToCapture(Space fromSpace, Space toSpace)
        {
            if (fromSpace == toSpace)
            {
                return false;
            }
            // if MOVE LIKE A ROOK
            if ((fromSpace.X == toSpace.X) || (fromSpace.Y == toSpace.Y))
            {
                // if space is to the right -> above?
                if (fromSpace.X > toSpace.X && fromSpace.Y == toSpace.Y)
                {
                    for (int i = fromSpace.X - 1; i >= toSpace.X; i--)
                    {
                        if (i == toSpace.X)
                        {
                            // capture or move to empty space
                            return true;
                        }
                        if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                else if (fromSpace.X < toSpace.X && fromSpace.Y == toSpace.Y)
                {
                    // if space is to the left -> below?
                    for (int i = fromSpace.X + 1; i <= toSpace.X; i++)
                    {
                        if (i == toSpace.X)
                        {
                            // capture or move to empty space
                            return true;
                        }
                        if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }


                }
                // if space is above -> to the left?
                else if (fromSpace.Y > toSpace.Y && fromSpace.X == toSpace.X)
                {
                    for (int i = fromSpace.Y - 1; i >= toSpace.Y; i--)
                    {
                        if (i == toSpace.Y)
                        {
                            // capture or move to empty space
                            return true;
                        }
                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                else if (fromSpace.Y < toSpace.Y && fromSpace.X == toSpace.X)
                {
                    // if space is below -> to the right??
                    for (int i = fromSpace.Y + 1; i <= toSpace.Y; i++)
                    {
                        if (i == toSpace.Y)
                        {
                            // capture or move to empty space
                            return true;
                        }
                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
            } // end MOVE LIKE A ROOK

            // MOVE LIKE A BISHOP
            if ((float)(Math.Abs(fromSpace.X - toSpace.X)) / (float)(Math.Abs(fromSpace.Y - toSpace.Y)) == 1)
            {
                // from toSpace up to fromSpace - if any spaces contain pieces, return false
                // if toSpace is down and right
                if (fromSpace.X < toSpace.X && fromSpace.Y < toSpace.Y)
                {
                    for (int i = fromSpace.X + 1, k = fromSpace.Y + 1; i <= toSpace.X && k <= toSpace.Y; i++, k++)
                    {
                        if ((i == toSpace.X || k == toSpace.Y))
                        {
                            // capture or move to empty space
                            return true;
                        }
                        if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // if toSpace is down and left
                else if (fromSpace.X < toSpace.X && fromSpace.Y > toSpace.Y)
                {
                    for (int i = fromSpace.X + 1, k = fromSpace.Y - 1; i <= toSpace.X && k >= toSpace.Y; i++, k--)
                    {
                        if ((i == toSpace.X || k == toSpace.Y))
                        {
                            // capture or move to empty space
                            return true;
                        }
                        if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // if toSpace is up and right
                else if (fromSpace.X > toSpace.X && fromSpace.Y < toSpace.Y)
                {
                    for (int i = fromSpace.X - 1, k = fromSpace.Y + 1; i >= toSpace.X && k <= toSpace.Y; i--, k++)
                    {
                        if ((i == toSpace.X || k == toSpace.Y))
                        {
                            // capture or move to empty space
                            return true;
                        }
                        if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // if toSpace is up and left
                else if (fromSpace.X > toSpace.X && fromSpace.Y > toSpace.Y)
                {
                    for (int i = fromSpace.X - 1, k = fromSpace.Y - 1; i >= toSpace.X && k >= toSpace.Y; i--, k--)
                    {
                        if ((i == toSpace.X || k == toSpace.Y))
                        {
                            // capture or move to empty space
                            return true;
                        }
                        if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // success
                return true;
            }
            // end MOVE LIKE A BISHOP

            // fallout condition
            return false;
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return fromSpace.Piece.belongsToPlayer != 
                toSpace.Piece.belongsToPlayer &&
                CanTryToCapture(fromSpace, toSpace);
        }
    }
}
