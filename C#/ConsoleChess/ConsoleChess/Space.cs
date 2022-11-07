using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    internal class Space
    {
        public int Latitude;
        public int Longitude;
        public Piece piece;

        public string PrintSpace()
        {
            if(piece == null)
            {
                return " ";
            }
            else
            {
                return piece.ToString();
            }
        }
    }
}
