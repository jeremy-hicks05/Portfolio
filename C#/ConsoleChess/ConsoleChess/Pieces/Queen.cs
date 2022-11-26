using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    internal class Queen : Piece
    {
        public Queen(string name, Player belongsTo) : base(name, belongsTo)
        {

        }

        //public override void MoveTo(Space spaceMovedTo)
        //{
        //    spaceMovedTo.Piece.Name = Name;
        //    Name = "[ ]";
        //}

        public override bool CanAttackSpace(Space fromSpace, Space toSpace)
        {
            return CanMoveFromSpaceToSpace(fromSpace, toSpace);
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
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
                        if (i == toSpace.X && toSpace.Piece.belongsToPlayer != belongsToPlayer)
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
                        if (i == toSpace.X && toSpace.Piece.belongsToPlayer != belongsToPlayer)
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
                        if (i == toSpace.Y && toSpace.Piece.belongsToPlayer != belongsToPlayer)
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
                        if (i == toSpace.Y && toSpace.Piece.belongsToPlayer != belongsToPlayer)
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
                // success
                //return true;
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
                        if ((i == toSpace.X || k == toSpace.Y) && Board.spaces[i][k].Piece.belongsToPlayer != belongsToPlayer)
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
                        if ((i == toSpace.X || k == toSpace.Y) && Board.spaces[i][k].Piece.belongsToPlayer != belongsToPlayer)
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
                        if ((i == toSpace.X || k == toSpace.Y) && Board.spaces[i][k].Piece.belongsToPlayer != belongsToPlayer)
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
                        if ((i == toSpace.X || k == toSpace.Y) && Board.spaces[i][k].Piece.belongsToPlayer != belongsToPlayer)
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
            return false;
        }
    }
}
