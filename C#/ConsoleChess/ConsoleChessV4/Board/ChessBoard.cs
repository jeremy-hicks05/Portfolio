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
    public class ChessBoard
    {
        public static ChessBoardSpace[,] Board;

        public ChessBoard()
        {
            Board = new ChessBoardSpace[8, 8];
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
            Board[5, 0].Piece = new Bishop() { Color = true };
            Board[6, 0].Piece = new Knight() { Color = true };
            Board[7, 0].Piece = new Rook() { Color = true };

            Board[0, 1].Piece = new Pawn() { Color = true };
            Board[1, 1].Piece = new Pawn() { Color = true };
            Board[2, 1].Piece = new Pawn() { Color = true };
            Board[3, 1].Piece = new Pawn() { Color = true };
            Board[4, 1].Piece = new Pawn() { Color = true };
            Board[5, 1].Piece = new Pawn() { Color = true };
            Board[6, 1].Piece = new Pawn() { Color = true };
            Board[7, 1].Piece = new Pawn() { Color = true };

            Board[7, 7].Piece = new Rook() { Color = false };
            Board[6, 7].Piece = new Knight() { Color = false };
            Board[5, 7].Piece = new Bishop() { Color = false };
            Board[4, 7].Piece = new Queen() { Color = false };
            Board[3, 7].Piece = new King() { Color = false };
            Board[2, 7].Piece = new Bishop() { Color = false };
            Board[1, 7].Piece = new Knight() { Color = false };
            Board[0, 7].Piece = new Rook() { Color = false };

            Board[7, 6].Piece = new Pawn() { Color = false };
            Board[6, 6].Piece = new Pawn() { Color = false };
            Board[4, 6].Piece = new Pawn() { Color = false };
            Board[3, 6].Piece = new Pawn() { Color = false };
            Board[2, 6].Piece = new Pawn() { Color = false };
            Board[1, 6].Piece = new Pawn() { Color = false };
            Board[0, 6].Piece = new Pawn() { Color = false };
            Board[5, 6].Piece = new Pawn() { Color = false };

        }

        public void PrintChessBoard()
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

        public bool IsLegalMove(Move move)
        {
            
            if (!move.startingSpace.HasAPiece())
            {
                Console.WriteLine("Starting space does not have a piece.  Please select another space.");
                return false;
            }
            else if(move.endingSpace.HasAPiece())
            {
                if(move.startingSpace.Piece!.Color == move.endingSpace.Piece!.Color)
                {
                    Console.WriteLine("Cannot capture your own piece");
                    return false;
                }
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
            //AbstractPiece tempPiece = toSpace.Piece;
            if (fromSpace.HasAPiece())
            {
                toSpace.Piece = fromSpace.Piece!;

                fromSpace.Piece = null;
            }
            
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
