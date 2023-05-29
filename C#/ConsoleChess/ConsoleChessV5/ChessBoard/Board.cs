using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV5.ChessBoard
{
    internal class Board
    {
        internal Space[,] Spaces = new Space[8, 8];
        public Board()
        {
            // initialize chessboard and pieces
        }

        internal void PrintBoard()
        {
            Console.WriteLine("Printing chess board");
            // TODO: Implement Printing Chess Board
        }

        internal bool MovePiece(Space firstSpace, Space secondSpace)
        {
            Console.WriteLine("Moving Piece . . .");
            throw new NotImplementedException();
        }
    }
}
