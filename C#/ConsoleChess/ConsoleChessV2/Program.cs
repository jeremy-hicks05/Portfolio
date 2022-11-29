namespace ConsoleChessV2
{
    internal class Program
    {
        static void Main()
        {
            // Initialize Board
            ChessBoard.InitBoard();
            ChessBoard.PrintBoard();
            while (true)
            {

                Space startingSpace = ChessBoard.UserSelectsSpace();
                Space endingSpace = ChessBoard.UserSelectsSpace();

                startingSpace.PrintInfo();
                endingSpace.PrintInfo();

                if (ChessBoard.TryToMove(startingSpace, endingSpace))
                {
                    ChessBoard.Move(startingSpace, endingSpace);
                }

                ChessBoard.PrintBoard();
            }
        }
    }
}