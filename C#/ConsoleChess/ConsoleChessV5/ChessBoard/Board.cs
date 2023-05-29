using ConsoleChessV5.Piece;
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
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Spaces[i, j] = new Space();
                }
            }

            Spaces[0, 1].Piece = new Pawn() { Owner = false };
            Spaces[1, 1].Piece = new Pawn() { Owner = false };
            Spaces[2, 1].Piece = new Pawn() { Owner = false };
            Spaces[3, 1].Piece = new Pawn() { Owner = false };
            Spaces[4, 1].Piece = new Pawn() { Owner = false };
            Spaces[5, 1].Piece = new Pawn() { Owner = false };
            Spaces[6, 1].Piece = new Pawn() { Owner = false };
            Spaces[7, 1].Piece = new Pawn() { Owner = false };
        }

        internal void PrintBoard()
        {
            Console.WriteLine("Printing chess board");
            // TODO: Implement Printing Chess Board
            for(int j = 0;j < 8;j++)
            {
                for(int i = 0;i < 8;i++)
                {
                    Console.Write(GetSpaceIcon(Spaces[i,j]));
                }
                Console.WriteLine();
            }
        }

        private string GetSpaceIcon(Space space)
        {
            if (space.Piece != null)
            {
                if(space.Piece.Owner == true)
                {
                    return space.Piece.Icon.ToUpper();
                }
                return space.Piece.Icon.ToLower();
            }
            return " ";
        }

        internal bool MovePiece(Space firstSpace, Space secondSpace)
        {
            Console.WriteLine("Moving Piece . . .");
            throw new NotImplementedException();
        }
    }
}
