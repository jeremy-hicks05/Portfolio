// TODO:
// Define Classes
//  Board
//  Pieces
//  Move Function
//  Ask for User Input


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

                // move piece
                Board.ChessBoard.MovePiece(
                    Board.ChessBoard.Board[startColumn, startRow],
                    Board.ChessBoard.Board[endColumn, endRow]
                    );

                // change turn or end game

                // print board state
                Board.ChessBoard.PrintChessBoard();
            }
        }
    }
}