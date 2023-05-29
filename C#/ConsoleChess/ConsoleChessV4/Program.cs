// TODO:
// Define Classes
//  Board
//  Pieces
//  Move Function
//  Ask for User Input


using ConsoleChessV4.Board;
using ConsoleChessV4.ChessMove;

namespace ConsoleChessV4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessGame.ChessGame chessGame = new ChessGame.ChessGame();

            chessGame.Board.PrintChessBoard();

            chessGame.Board.PrintChessBoard();
        }
    }
}