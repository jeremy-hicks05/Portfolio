using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    internal class Queen : Piece
    {
        public Queen(string name, Player belongsTo) : base(name, belongsTo)
        {

        }

        public override void MoveTo(Space spaceMovedTo)
        {
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // if MOVE LIKE A ROOK
            if((fromSpace.X == toSpace.X) || (fromSpace.Y == toSpace.Y))
            {
                // if space is to the right
                if (fromSpace.X > toSpace.X)
                {
                    for (int i = fromSpace.X - 1; i >= toSpace.X; i--)
                    {
                        if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                else if (fromSpace.X < toSpace.X)
                {
                    // if space is to the left
                    for (int i = fromSpace.X + 1; i <= toSpace.X; i++)
                    {
                        if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }


                }
                // if space is above
                else if (fromSpace.Y > toSpace.Y)
                {
                    for (int i = fromSpace.Y + 1; i <= toSpace.Y; i++)
                    {
                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                else if (fromSpace.Y < toSpace.Y)
                {
                    // if space is below
                    for (int i = fromSpace.Y - 1; i >= toSpace.Y; i--)
                    {
                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // success
                return true;
            } // end MOVE LIKE A ROOK
            // MOVE LIKE A BISHOP
            else if((Math.Abs(fromSpace.X - toSpace.X)) / (Math.Abs(fromSpace.Y - toSpace.Y)) == 1)
            {
                // from toSpace up to fromSpace - if any spaces contain pieces, return false
                // if toSpace is down and right
                if (fromSpace.X < toSpace.X && fromSpace.Y < toSpace.Y)
                {
                    for (int i = toSpace.X, k = toSpace.Y; i > fromSpace.X && k > fromSpace.Y; i--, k--)
                    {
                        if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // if toSpace is down and left
                else if (fromSpace.X < toSpace.X && fromSpace.Y > toSpace.Y)
                {
                    for (int i = toSpace.X, k = toSpace.Y; i > fromSpace.X && k < fromSpace.Y; i--, k++)
                    {
                        if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // if toSpace is up and right
                else if (fromSpace.X > toSpace.X && fromSpace.Y < toSpace.Y)
                {
                    for (int i = toSpace.X, k = toSpace.Y; i < fromSpace.X && k > fromSpace.Y; i++, k--)
                    {
                        if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // if toSpace is up and left
                else if (fromSpace.X > toSpace.X && fromSpace.Y > toSpace.Y)
                {
                    for (int i = toSpace.X, k = toSpace.Y; i < fromSpace.X && k < fromSpace.Y; i++, k++)
                    {
                        if (Board.spaces[i][k].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
                // end MOVE LIKE A BISHOP
            return false;
        }
    }
}
