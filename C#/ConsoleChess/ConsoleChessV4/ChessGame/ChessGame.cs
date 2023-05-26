using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.ChessGame
{
    public static class ChessGame
    {
        public static bool Turn { get; set; }
        public static string State { get; set; } // Win, Draw, BlackTurn, WhiteTurn
    }
}
