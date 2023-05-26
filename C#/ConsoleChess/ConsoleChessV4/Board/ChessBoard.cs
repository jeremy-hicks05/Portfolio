using ConsoleChessV4.Abstract;
using ConsoleChessV4.ChessMove;
using ConsoleChessV4.Piece;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
                    Board[i, j].Row = i;
                    Board[i, j].Column = j;
                }
            }
            Board[0, 0].Piece = new Rook() { Color = true };

            Board[1, 0].Piece = new Knight() { Color = true };
            Board[2, 0].Piece = new Bishop() { Color = true };
            Board[3, 0].Piece = new Queen() { Color = true };
            Board[4, 0].Piece = new King() { Color = true };
            Board[4, 0].Piece = new Bishop() { Color = true };
            Board[4, 0].Piece = new Knight() { Color = true };
            Board[4, 0].Piece = new Rook() { Color = true };

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

        public static bool IsLegalMove(Move move)
        {
            if(!move.startingSpace.HasAPiece())
            {
                Console.WriteLine("Starting space does not have a piece.  Please select another space.");
                return false;
            }
            else if(move.startingSpace.Piece is Rook && IsRookMove(move))
            {
                return true;
            }
            else if (move.startingSpace.Piece is Bishop && IsBishopMove(move))
            {
                return true;
            }
            else if (move.startingSpace.Piece is Pawn && IsPawnMove(move))
            {
                return true;
            }
            else if (move.startingSpace.Piece is King && IsKingMove(move))
            {
                return true;
            }
            else if (move.startingSpace.Piece is Queen && IsQueenMove(move))
            {
                return true;
            }
            else if (move.startingSpace.Piece is Knight && IsKnightMove(move))
            {
                return true;
            }
            return false;
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
            if(space.Piece.Color == false)
            {
                return char.Parse(space.Piece.PieceIcon.ToString().ToLower());
            }
            return space.Piece.PieceIcon;
        }

        public static ChessBoardSpace GetChessBoardSpace(int row, int column)
        {
            return Board[row, column];
        }

        private static bool IsRookMove(Move move)
        {
            return move.startingSpace != move.endingSpace && 
                (move.startingSpace.Column == move.endingSpace.Column
                || move.startingSpace.Row == move.endingSpace.Row);
        }

        public static bool IsBishopMove(Move move)
        {
            float slope = Math.Abs(
                        (move.startingSpace.Column - move.endingSpace.Column) /
                        (move.startingSpace.Row - move.endingSpace.Row));
            return move.startingSpace != move.endingSpace &&
                slope == 1;
        }

        public static bool IsPawnMove(Move move)
        {
            if(move.startingSpace.Piece.HasMoved)
            {
                // only allow capture and moving up one space
            }
            return false;
        }

        public static bool IsKnightMove(Move move)
        {
            return false;
        }

        public static bool IsKingMove(Move move)
        {
            return false;
        }

        public static bool IsQueenMove(Move move)
        {
            return IsRookMove(move) || IsBishopMove(move);
        }
    }
}
