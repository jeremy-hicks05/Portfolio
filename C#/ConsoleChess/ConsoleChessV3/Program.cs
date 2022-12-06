namespace ConsoleChessV3
{
    using static ChessBoard;
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Chess!");
            Console.ReadLine();
            InitBoard();
            PrintBoard();

            Console.WriteLine("Exiting Chess");
            Console.ReadLine();
        }
    }
}