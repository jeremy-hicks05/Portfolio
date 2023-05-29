using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleChessV5.Abstract;
using ConsoleChessV5.ChessBoard;
using static ConsoleChessV5.Utility.Enums;

namespace ConsoleChessV5
{
    internal class ChessGame
    {
        // properties
        internal Board ChessBoard { get; set; }

        State GameState;

        internal Stack<Move> MovesPlayed = new Stack<Move>();

        // constructors
        internal ChessGame()
        {
            ChessBoard = new Board();
            GameState = State.Playing;

            while(GameState == State.Playing) 
            {
                GameState = UpdateGameState();
            }
        }

        // methods
        internal Space GetFirstSpace()
        {
            string column = GetUserInputColumn();

            string row = GetUserInputRow();

            return new Space(column, row);
        }

        internal Space GetSecondSpace()
        {
            string column = GetUserInputColumn();

            string row = GetUserInputRow();
            return new Space(column, row);
        }

        private string GetUserInputColumn()
        {
            Console.WriteLine("Please enter column (A-H)");
            string input = Console.ReadLine() ?? string.Empty;
            return input;
        }

        internal string GetUserInputRow()
        {
            Console.WriteLine("Please enter row (1-8)");
            string input = Console.ReadLine() ?? string.Empty;
            return input;
        }

        private State UpdateGameState()
        {
            return State.Playing;
        }
    }
}
