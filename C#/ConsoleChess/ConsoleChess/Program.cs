using ConsoleChess.Enums;
using ConsoleChess.Interfaces;

namespace ConsoleChess
{
    /* TODO
     *  1. Fix IsUnderAttackByWhite being false when a piece of the same player is 'protecting' their piece
     *  2. Change 'hasMoved' to a Move() function that checks CanMoveToSpace first
     *  3. Make CheckAllSpacesDefendedByWhite function?
     *  Current issue - if pieces 'can attack' their own, they can defend 'through' them
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

                Piece selectedPiece = Board.spaces[stLat][stLong].Piece;
                Console.WriteLine("Piece info: ");
                Console.WriteLine(selectedPiece.Name + " on space " + stLat + " " + stLong + " belongs to player " + selectedPiece.belongsToPlayer);
                Console.WriteLine("Space info: \nIs Under Attack by White: " + Board.spaces[stLat][stLong].IsUnderAttackByWhite + "\n Is Under Attack by Black: " + Board.spaces[stLat][stLong].IsUnderAttackByBlack);

                Console.Write("Enter Letter for Space to be moved to:");
                int enLong = Board.NotationToInt(Console.ReadLine());
                Console.Write("Enter Number for Space to be moved to:");
                int enLat = Board.NotationToInt(Console.ReadLine());

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