using ConsoleChess.Enums;

namespace ConsoleChess
{
    /* TODO:
     * 1. Prevent pieces from moving when they are blocked
     * 2. Switch from [0, 0] notation to A8 D2 etc.
     * 3. 
    */
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Chess!");
            Board.InitBoard();

            System.ConsoleKey playing = ConsoleKey.Y;
            while (playing == ConsoleKey.Y)
            {
                int startLat;
                int endLat;
                int startLong;
                int endLong;

                
                Console.Write("Enter Lat and Long for Piece to be moved:");
                string stLat = Console.ReadLine();
                if (int.TryParse(stLat, out startLat))
                {
                    //Console.WriteLine("Accepted input");
                    string stLong = Console.ReadLine();
                    if (int.TryParse(stLong, out startLong))
                    {
                        //Console.WriteLine("Accepted input");
                        Console.Write("Enter Lat and Long for destination Space:");
                        string enLat = Console.ReadLine();
                        if (int.TryParse(enLat, out endLat))
                        {
                            //Console.WriteLine("Accepted input");
                            string enLong = Console.ReadLine();
                            if (int.TryParse(enLong, out endLong))
                            {
                                if (Board.spaces[startLat][startLong].Piece.CanMoveFromSpaceToSpace(
                                    Board.spaces[startLat][startLong],
                                    Board.spaces[endLat][endLong]))
                                {
                                    Board.MovePieceFromSpaceToSpace(Board.spaces[startLat][startLong], Board.spaces[endLat][endLong]);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Try again");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Try again");
                    }
                }
                else
                {
                    Console.WriteLine("Try again");
                }

                Console.Clear();

                Board.PrintBoard();

                Console.Write("Keep playing?  Y or N:");
                playing = Console.ReadKey().Key;
                //Console.Clear();
            }
        }
    }
}