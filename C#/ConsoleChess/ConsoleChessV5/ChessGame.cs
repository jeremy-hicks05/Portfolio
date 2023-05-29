using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV5
{
    internal class ChessGame
    {
        // properties
        internal Board ChessBoard { get; set; } = new Board();
        internal enum State { WhiteWins, BlackWins, Playing, Draw }

        internal Stack<Move> MovesPlayed = new Stack<Move>();

        // methods
        internal Space GetFirstSpace()
        {
            Console.WriteLine("Please enter Column (A-H)");

            Console.WriteLine("Please enter Row (1-8)");
            return new Space("A", "1");
        }
    }
}
