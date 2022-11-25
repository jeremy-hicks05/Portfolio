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
                //int startLat;
                //int endLat;
                //int startLong;
                //int endLong;

                // if input is 'A' -> translate to 7 for X value
                // if input is '1' -> translate to 0 for Y value (may need to swap these?)

                Console.Write("Enter Lat and Long for Piece to be moved:");
                int stLong = Board.NotationToInt(Console.ReadLine());
                int stLat = Board.NotationToInt(Console.ReadLine());
                
                

                Console.Write("Enter Lat and Long for destination Space:");
                int enLong = Board.NotationToInt(Console.ReadLine());
                int enLat = Board.NotationToInt(Console.ReadLine());
                
                if (Board.spaces[stLat][stLong].Piece.CanMoveFromSpaceToSpace(
                    Board.spaces[stLat][stLong],
                    Board.spaces[enLat][enLong]))
                {
                    Board.MovePieceFromSpaceToSpace(Board.spaces[stLat][stLong], Board.spaces[enLat][enLong]);
                }


                Console.Clear();

                Board.PrintBoard();

                Console.Write("Keep playing?  Y or N:");
                playing = Console.ReadKey().Key;
            }
        }
    }
}