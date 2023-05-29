using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV5
{
    internal class Board
    {
        internal Space[,] Spaces = new Space[8, 8];
        public Board() 
        {
            
        }

        internal void PrintBoard()
        {
            Console.WriteLine("Printing chess board");
        }

        internal void MovePiece(Space firstSpace, Space secondSpace)
        {
            Console.WriteLine("Moving Piece . . .");
        }

    }
}
