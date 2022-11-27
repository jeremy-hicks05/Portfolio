using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    internal class Rook : Piece
    {
        public Rook(string name, Player belongsTo) : base(name, belongsTo)
        {
            hasMoved = false;
        }

        public override bool CanAttackSpace(Space fromSpace, Space toSpace)
        {
            if (fromSpace.X == toSpace.X && fromSpace.Y == toSpace.Y)
            {
                return false;
            }
            else if (fromSpace.X == toSpace.X || fromSpace.Y == toSpace.Y)
            {
                // if space is above
                if (fromSpace.X > toSpace.X)
                {
                    for (int i = fromSpace.X - 1; i >= toSpace.X; i--)
                    {
                        if (i == toSpace.X && (toSpace.Piece.belongsToPlayer != Player.None))
                        {
                            return true;
                        }

                        if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                else if (fromSpace.X < toSpace.X)
                {
                    // if space is below
                    for (int i = fromSpace.X + 1; i <= toSpace.X; i++)
                    {
                        if (i == toSpace.X && (toSpace.Piece.belongsToPlayer != Player.None))
                        {
                            return true;
                        }

                        if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // if space is to the left
                else if (fromSpace.Y > toSpace.Y)
                {
                    for (int i = fromSpace.Y - 1; i >= toSpace.Y; i++)
                    {
                        if (i == toSpace.Y && (toSpace.Piece.belongsToPlayer != Player.None))
                        {
                            return true;
                        }

                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                else if (fromSpace.Y < toSpace.Y)
                {
                    // if space is to the right
                    for (int i = fromSpace.Y + 1; i <= toSpace.Y; i--)
                    {
                        if (i == toSpace.Y && (toSpace.Piece.belongsToPlayer != Player.None))
                        {
                            return true;
                        }

                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            //return false;
            if (fromSpace.X == toSpace.X && fromSpace.Y == toSpace.Y)
            {
                return false;
            }
            else if (fromSpace.X == toSpace.X || fromSpace.Y == toSpace.Y)
            {
                // if space is above
                if (fromSpace.X > toSpace.X)
                {
                    for (int i = fromSpace.X - 1; i >= toSpace.X; i--)
                    {
                        if (i == toSpace.X && belongsToPlayer != toSpace.Piece.belongsToPlayer)
                        {
                            return true;
                        }

                        if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                else if (fromSpace.X < toSpace.X)
                {
                    // if space is below
                    for (int i = fromSpace.X + 1; i <= toSpace.X; i++)
                    {
                        if (i == toSpace.X && belongsToPlayer != toSpace.Piece.belongsToPlayer)
                        {
                            return true;
                        }

                        if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // if space is to the left
                else if (fromSpace.Y > toSpace.Y)
                {
                    for (int i = fromSpace.Y - 1; i >= toSpace.Y; i--)
                    {
                        if (i == toSpace.Y && (belongsToPlayer != toSpace.Piece.belongsToPlayer))
                        {
                            return true;
                        }

                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                else if (fromSpace.Y < toSpace.Y)
                {
                    // if space is to the right
                    for (int i = fromSpace.Y + 1; i <= toSpace.Y; i++)
                    {
                        if ((i == toSpace.Y && belongsToPlayer != toSpace.Piece.belongsToPlayer))
                        {
                            return true;
                        }

                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}
