﻿using ConsoleChess.Interfaces;
using ConsoleChess.Enums;
using System.Xml.Linq;

namespace ConsoleChess.Pieces
{
    internal class Bishop : Piece
    {
        public Bishop(string name, Player belongsTo) : base(name, belongsTo)
        {

        }

        public override bool CanAttackSpace(Space fromSpace, Space toSpace)
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
            return CanAttackSpace(fromSpace, toSpace) &&
                fromSpace.Piece.belongsToPlayer != toSpace.Piece.belongsToPlayer;
        }
    }
}
