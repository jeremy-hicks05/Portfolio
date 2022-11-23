using ConsoleChess.Interfaces;
using ConsoleChess.Players;
using System.Xml.Linq;

namespace ConsoleChess.Pieces
{
    internal class Bishop : Piece
    {
        public Bishop(string name, Player belongsTo) : base(name, belongsTo)
        {

        }

        public override void MoveTo(Space spaceMovedTo)
        {
            // move like a bishop
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // +1 +1
            // +2 +2
            // -1 -1
            // -2 -2
            // +3 +3
            // -3 -3
            // difference / difference == 1?


            if((Math.Abs(fromSpace.X - toSpace.X)) / (Math.Abs(fromSpace.Y - toSpace.Y)) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
