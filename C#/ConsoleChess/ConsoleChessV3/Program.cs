/********************************************
 *     ** Console Chess **                  *
 *      by Jeremy Hicks (c) 2022            *
 *      Tested By: Kari Seitz               *
 *      Early Version Review: Shaun Lake    *
 *                                          *
 *                                          *
 ********************************************/


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
                BlackIsCheckMated();
                WhiteIsCheckMated();
                BlackIsStaleMated();
                WhiteIsStaleMated();
                PrintBoard();

                GetInitialSpaceInput();
                GetTargetSpaceInput();

                PlayMove();
            }
        }
    }
}