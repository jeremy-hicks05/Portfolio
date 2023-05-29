using ConsoleChessV4.Board;
using ConsoleChessV4.ChessGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.ChessMove
{
    public class Move
    {
        public Player.Player player;
        public bool turn;
        public ChessBoardSpace startingSpace;
        public ChessBoardSpace endingSpace;

        public Move(
            Player.Player player,
            bool turn,
            ChessBoardSpace startingSpace,
            ChessBoardSpace endingSpace)
        {
            this.player = player;
            this.turn = turn;
            this.startingSpace = startingSpace;
            this.endingSpace = endingSpace;
        }
    }
}
