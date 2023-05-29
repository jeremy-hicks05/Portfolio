﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.Board
{
    public class ChessBoardSpace
    {
        public Abstract.Piece? Piece { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public bool HasAPiece()
        {
            return this.Piece != null;
        }

        public bool IsEmpty()
        {
            return this.Piece == null;
        }
    }
}
