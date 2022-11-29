using ConsoleChess.Interfaces;
using ConsoleChess.Enums;
using System.Xml.Linq;

namespace ConsoleChess.Pieces
{
    internal class Bishop : Piece
    {
        public Bishop(string name, Player belongsTo) : base(name, belongsTo)
        {

        }

        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // test whether attempted move follows the rules of chess
            if (fromSpace == toSpace)
            {
                return false;
            }
            else if (fromSpace.X == toSpace.X || fromSpace.Y == toSpace.Y)
            {
                return false;
            }
            else if ((float)(Math.Abs(fromSpace.X - toSpace.X)) / (float)(Math.Abs(fromSpace.Y - toSpace.Y)) == 1)
            {
                return true;
            }
            return false;
        }

        public override bool HasPiecesBlockingMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
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
            // fallout true - blocked
            return true;
        }

        public override bool CanTryToCapture(Space fromSpace, Space toSpace)
        {
            if (fromSpace == toSpace)
            {
                return false;
            }
            if (fromSpace.X == toSpace.X || fromSpace.Y == toSpace.Y)
            {
                return false;
            }
            if ((float)(Math.Abs(fromSpace.X - toSpace.X)) / (float)(Math.Abs(fromSpace.Y - toSpace.Y)) == 1)
            {
                // if toSpace is down and right
                if (fromSpace.X < toSpace.X && fromSpace.Y < toSpace.Y)
                {
                    for (int i = fromSpace.X + 1, k = fromSpace.Y + 1; i <= toSpace.X && k <= toSpace.Y; i++, k++)
                    {
                        if (i == toSpace.X || k == toSpace.Y)
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
            }
            return false;
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return CanTryToCapture(fromSpace, toSpace) &&
                fromSpace.Piece.belongsToPlayer != toSpace.Piece.belongsToPlayer;
        }
    }
}
