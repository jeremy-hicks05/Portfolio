using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess
{
    internal class Space
    {
        public int X { get; set; }
        public int Y { get; set; }
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
