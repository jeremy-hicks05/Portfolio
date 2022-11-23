﻿using ConsoleChess.Interfaces;
using ConsoleChess.Players;

namespace ConsoleChess
{
    internal class Space
    {
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public Piece Piece { get; set; }

        public string PrintSpace()
        {
            if(Piece == null)
            {
                return "[ ]";
            }
            else
            {
                return Piece.Name;
            }
        }

        public void MoveTo(Space spaceMovedTo)
        {
            spaceMovedTo.Piece = Piece;
            ClearSpace();
        }

        public void MovePieceFromMeToSpace(Space toSpace)
        {
            if(Piece.CanMoveFromSpaceToSpace(this, toSpace))
            {

            }
        }

        public void ClearSpace()
        {
            Piece = new Piece("[ ]", Player.None);
        }
    }
}
