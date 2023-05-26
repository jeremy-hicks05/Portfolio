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
            Board[0, 0].PieceLetter = 'R';
        }

        public static void PrintChessBoard()
        {
            Console.WriteLine("_________________________________");
            for (int i = 0; i< Board.GetLength(0); i++)
            {
                Console.Write("| ");
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    Console.Write(Board[i, j].PieceLetter);
                    Console.Write(" | ");
                }
                Console.WriteLine();
                Console.WriteLine("_________________________________");
            }
        }

        public static bool MovePiece(ChessBoardSpace fromSpace, ChessBoardSpace toSpace)
        {
            toSpace.PieceLetter = fromSpace.PieceLetter;
            fromSpace.PieceLetter = ' ';
            return true;
        }
    }
}
