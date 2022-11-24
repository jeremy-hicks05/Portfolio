using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    internal class Rook : Piece
    {
        public Rook(string name, Player belongsTo) : base(name, belongsTo)
        {

        }

        public override void MoveTo(Space spaceMovedTo)
        {
            // move like a rook
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if(fromSpace.X == toSpace.X || fromSpace.Y == toSpace.Y)
            {
                // if space is to the right
                if (fromSpace.X > toSpace.X)
                {
                    for (int i = fromSpace.X - 1; i >= toSpace.X; i--)
                    {
                        if ((toSpace.Piece.belongsToPlayer != Player.None) && (i == toSpace.X && belongsToPlayer != toSpace.Piece.belongsToPlayer))
                        {
                            // capture piece
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
                    // if space is to the left
                    for (int i = fromSpace.X + 1; i <= toSpace.X; i++)
                    {
                        if ((toSpace.Piece.belongsToPlayer != Player.None) && (i == toSpace.X && belongsToPlayer != toSpace.Piece.belongsToPlayer))
                        {
                            // capture piece
                            return true;
                        }

                        if (Board.spaces[i][toSpace.Y].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // if space is above
                else if (fromSpace.Y > toSpace.Y)
                {
                    for (int i = fromSpace.Y - 1; i <= toSpace.Y; i++)
                    {
                        if ((toSpace.Piece.belongsToPlayer != Player.None) && (i == toSpace.Y && belongsToPlayer != toSpace.Piece.belongsToPlayer))
                        {
                            // capture piece
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
                    // if space is below
                    for (int i = fromSpace.Y + 1; i >= toSpace.Y; i--)
                    {
                        if ((i == toSpace.Y && belongsToPlayer != toSpace.Piece.belongsToPlayer))
                        {
                            // move to blank space or capture piece
                            return true;
                        }

                        if (Board.spaces[toSpace.X][i].Piece.belongsToPlayer != Player.None)
                        {
                            return false;
                        }
                    }
                }
                // success
                return true;
            }
            return false;
        }
    }
}
