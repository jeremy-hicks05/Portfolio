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

            while (true)
            {
                PrintBoard();

                GetInitialSpaceInput();
                GetTargetSpaceInput();

                PlayMove();
                //PrintBoard();
                //SaveMoveInHistory();

                ShowMoveHistory();
                //TakeBackMove();
                //PrintBoard();
                //ShowMoveHistory();
                Console.ReadLine();
            }
            //Console.WriteLine("Exiting Chess");
        }
    }
}