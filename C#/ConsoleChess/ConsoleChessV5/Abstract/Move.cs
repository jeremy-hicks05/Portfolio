﻿using ConsoleChessV5.ChessBoard;
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
        Space FirstSpace { get; set; }
        Space SecondSpace { get; set; }
        internal Move()
        {

        }

        internal Move(Space firstSpace, Space secondSpace)
        {

        }

        internal static Move MoveFactory(Space firstSpace, Space secondSpace)
        {
            if(firstSpace.Piece is Pawn)
            {
                return new Moves.EnPassant();
            }
            if(firstSpace.Piece.Owner != secondSpace.Piece.Owner)
            {
                return new Moves.Capture();
            }
            return new Moves.ToEmptySpace();
        }

        internal static bool IsLegalAttempt(Move move)
        {
            return true;
        }
    }
}
