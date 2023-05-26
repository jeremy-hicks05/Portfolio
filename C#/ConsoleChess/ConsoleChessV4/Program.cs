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
            Board.ChessBoard.InitChessBoard();
            Board.ChessBoard.PrintChessBoard();

            PlayChess();
            Board.ChessBoard.PrintChessBoard();
        }

        public static void PlayChess()
        {
            while (ChessGame.ChessGame.State != "BlackWin" &&
                    ChessGame.ChessGame.State != "WhiteWin")
            {
                // ask user for column for piece to move (a-h)
                //;
                int startColumn = Utility.UserInput.GetColumnInput();

                // ask user for row for piece to move (1-8)
                int startRow = Utility.UserInput.GetRowInput();
                //GetRowInput();

                // ask user for column for space to move to (a-h)
                //GetColumnInput();
                int endColumn = Utility.UserInput.GetColumnInput();

                // ask user for row for space to move to (1-8)
                //GetRowInput();
                int endRow = Utility.UserInput.GetRowInput();

                // check if piece on selected space can move to second space

                Move myMove = new Move(
                    null, 
                    ChessGame.ChessGame.Turn,
                    ChessBoard.Board[startColumn, startRow],
                    ChessBoard.Board[endColumn, endRow]);

                if(ChessBoard.IsLegalMove(myMove))
                {
                    // move piece
                    ChessBoard.MovePiece(
                        ChessBoard.Board[startColumn, startRow],
                        ChessBoard.Board[endColumn, endRow]);

                    // change turn or end game
                }

                // print board state
                ChessBoard.PrintChessBoard();
            }
        }
    }
}