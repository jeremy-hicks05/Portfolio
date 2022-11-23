namespace ConsoleChess
{
    /* TODO:
     * 1. Prevent pieces from moving when they are blocked
    */
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Chess!");
            Match myMatch = new Match();

            System.ConsoleKey playing = ConsoleKey.Y;
            while (playing == ConsoleKey.Y)
            {
                int startLat;
                int endLat;
                int startLong;
                int endLong;

                
                Console.Write("Enter Lat and Long for Piece to be moved:");
                string stLat = Console.ReadLine();
                if (Int32.TryParse(stLat, out startLat))
                {
                    //Console.WriteLine("Accepted input");
                    string stLong = Console.ReadLine();
                    if (Int32.TryParse(stLong, out startLong))
                    {
                        //Console.WriteLine("Accepted input");
                        Console.Write("Enter Lat and Long for destination Space:");
                        string enLat = Console.ReadLine();
                        if (Int32.TryParse(enLat, out endLat))
                        {
                            //Console.WriteLine("Accepted input");
                            string enLong = Console.ReadLine();
                            if (Int32.TryParse(enLong, out endLong))
                            {
                                if (myMatch.Board.spaces[startLat][startLong].Piece.CanMoveFromSpaceToSpace(
                                    myMatch.Board.spaces[startLat][startLong],
                                    myMatch.Board.spaces[endLat][endLong]))
                                {
                                    myMatch.Board.MovePieceFromSpaceToSpace(myMatch.Board.spaces[startLat][startLong], myMatch.Board.spaces[endLat][endLong]);
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

                myMatch.Board.PrintBoard();

                Console.Write("Keep playing?  Y or N:");
                playing = Console.ReadKey().Key;
                //Console.Clear();
            }
        }
    }
}