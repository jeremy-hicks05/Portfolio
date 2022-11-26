using ConsoleChess.Enums;
using ConsoleChess.Interfaces;

namespace ConsoleChess
{
    /* TODO
     *  
    */
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Welcome to Chess!");
            Board.InitBoard();

            System.ConsoleKey playing = ConsoleKey.Y;
            while (playing == ConsoleKey.Y)
            {
                //Board.FindAllSpacesAttacked();
                // if input is 'A' -> translate to 7 for X value
                // if input is '1' -> translate to 0 for Y value (may need to swap these?)
                Console.WriteLine();
                Console.Write("Enter Letter for Piece to be moved:");
                int stLong = Board.NotationToInt(Console.ReadLine());
                Console.Write("Enter Number for Piece to be moved:");
                int stLat = Board.NotationToInt(Console.ReadLine());

                Console.Write("Enter Letter for Space to be moved to:");
                int enLong = Board.NotationToInt(Console.ReadLine());
                Console.Write("Enter Number for Space to be moved to:");
                int enLat = Board.NotationToInt(Console.ReadLine());

                Piece selectedPiece = Board.spaces[stLat][stLong].Piece;
                Space startingSpace = Board.spaces[stLat][stLong];
                Space destinationSpace = Board.spaces[enLat][enLong];

                if (selectedPiece.CanMoveFromSpaceToSpace(
                    startingSpace,
                    destinationSpace))
                {
                    Board.MovePieceFromSpaceToSpace(startingSpace, destinationSpace);
                }

                Console.Clear();
                Board.FindAllSpacesAttacked();
                Board.PrintBoard();
                //Console.WriteLine();

                Console.Write("Keep playing?  Y or N:");
                playing = Console.ReadKey().Key;
            }
        }
    }
}