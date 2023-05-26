using ConsoleChessV4.Abstract;
using ConsoleChessV4.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.Board
{
    internal static class ChessBoard
    {
        internal static ChessBoardSpace[,] Board = new ChessBoardSpace[8, 8];

        public static void InitChessBoard()
        {

            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    Board[i, j] = new ChessBoardSpace();
                }
            }
            Board[0, 0].Piece = new Rook();
        }

        public static void PrintChessBoard()
        {
            Console.WriteLine("_________________________________");
            for (int j = Board.GetLength(1) - 1; j >= 0; j--)
            {
                Console.Write("| ");
                for (int i = 0; i < Board.GetLength(0); i++)
                {
                    Console.Write(PrintSpace(Board[i, j]));
                    Console.Write(" | ");
                }
                Console.WriteLine();
                Console.WriteLine("_________________________________");
            }
        }

        public static bool MovePiece(ChessBoardSpace fromSpace, ChessBoardSpace toSpace)
        {
            AbstractPiece tempPiece = toSpace.Piece;
            if (fromSpace.HasAPiece())
            {
                toSpace.Piece = fromSpace.Piece!;
            }
            fromSpace.Piece = tempPiece;
            return true;
        }

        public static char PrintSpace(ChessBoardSpace space)
        {
            if(space.Piece == null)
            {
                return ' ';
            }
            return space.Piece.PieceIcon;
        }
    }
}
