using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess
{
    internal class Space
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Piece Piece { get; set; }

        public bool IsUnderAttackByBlack = false;
        public bool IsUnderAttackByWhite = false;

        public Space()
        {
            X = -1;
            Y = -1;
            Piece = new Piece(" ", Player.None);
        }

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

        public void ClearSpace()
        {
            Piece = new Piece("[ ]", Player.None);
        }
    }
}
