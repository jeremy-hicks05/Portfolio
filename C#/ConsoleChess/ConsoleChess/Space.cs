using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleChess.Interfaces;

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
                return " ";
            }
            else
            {
                return Piece.ToString();
            }
        }

        public void MoveTo(Space spaceMovedTo)
        {
            spaceMovedTo.Piece = Piece;
            Piece = new Piece("[ ]");
        }

        public void MovePieceFromMeToSpace(Space toSpace)
        {
            if(Piece.CanMoveFromSpaceToSpace(this, toSpace))
            {

            }
        }

        public void Clear()
        {
            this.Piece = new Piece("[ ]");
        }
    }
}
