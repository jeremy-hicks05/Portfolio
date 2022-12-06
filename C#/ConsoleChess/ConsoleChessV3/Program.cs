namespace ConsoleChessV3
{
    using static ChessBoard;
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Chess!");
            Console.ReadLine();
            InitBoard();

            PrintBoard();

            GetInitialSpaceInput();
            GetTargetSpaceInput();

            PlayMove();

            SaveMoveInHistory();

            PrintBoard();

            Console.WriteLine("Exiting Chess");
            Console.ReadLine();
        }
    }
}