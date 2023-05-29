using ConsoleChessV5.ChessBoard;
using ConsoleChessV5.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleChessV5.Abstract
{
    internal abstract class Move
    {
        internal Space FirstSpace { get; set; }
        internal Space SecondSpace { get; set; }

        internal Move(Space firstSpace, Space secondSpace)
        {
            FirstSpace = firstSpace;
            SecondSpace = secondSpace;
        }

        internal static Move MoveFactory(Space firstSpace, Space secondSpace)
        {
            if(firstSpace.Piece is Pawn)
            {
                return new Moves.EnPassant(firstSpace, secondSpace);
            }
            if(firstSpace.Piece.Owner != secondSpace.Piece.Owner)
            {
                return new Moves.Capture(firstSpace, secondSpace);
            }
            return new Moves.ToEmptySpace(firstSpace, secondSpace);
        }

        internal static bool IsLegalAttempt(Move move)
        {
            if(move.FirstSpace.Piece is Pawn)
            {

            }
            return true;
        }
    }
}
