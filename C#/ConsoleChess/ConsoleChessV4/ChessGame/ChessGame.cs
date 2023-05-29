using ConsoleChessV4.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.ChessGame
{
    public class ChessGame
    {
        public ChessBoard Board = new ChessBoard();
        public bool Turn { get; set; }
        public string State { get; set; } // Win, Draw, BlackTurn, WhiteTurn

        public List<Turn.Turn> Turns { get; set; }
    }
}
