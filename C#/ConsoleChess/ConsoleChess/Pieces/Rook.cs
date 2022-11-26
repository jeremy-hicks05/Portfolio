using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    internal class Rook : Piece
    {
        //public bool hasMoved;

        /*  TODO: 
         *  1. Enable castling K side
         *  2. Enable castling Q side
         */

        /// <summary>

        /// </summary>
        /// <param name="name"></param>
        /// <param name="belongsTo"></param>
        public Rook(string name, Player belongsTo) : base(name, belongsTo)
        {
            hasMoved = false;
        }

        public override bool CanAttackSpace(Space fromSpace, Space toSpace)
        {
            // done

            //return false;
            //return CanMoveFromSpaceToSpace(fromSpace, toSpace);
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
                            // move to blank space or capture piece
                            //hasMoved = true;
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
                            // move to blank space or capture piece
                            //hasMoved = true;
                            return true;
                        }

                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // success
                //return true;
                //return false;
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
                        if ((toSpace.Piece.belongsToPlayer != Player.None) && (i == toSpace.X && belongsToPlayer != toSpace.Piece.belongsToPlayer))
                        {
                            // move to blank space or capture piece
                            //hasMoved = true;
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
                            // move to blank space or capture piece
                            //hasMoved = true;
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
                        if (i == toSpace.Y && (belongsToPlayer != toSpace.Piece.belongsToPlayer))
                        {
                            // move to blank space or capture piece
                            //hasMoved = true;
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
                        if ((i == toSpace.Y && belongsToPlayer != toSpace.Piece.belongsToPlayer))
                        {
                            // move to blank space or capture piece
                            //hasMoved = true;
                            return true;
                        }

                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // success
                //hasMoved = true;
                return true;
            }
            return false;
        }
    }
}
