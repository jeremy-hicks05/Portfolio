using ConsoleChess.Enums;
using ConsoleChess.Interfaces;

namespace ConsoleChess
{
    /* Current Rules Employed:
     * 1. Pawn up 2 on first move
     * 2. King cannot move into check
     * 3. All piece movement restrictions
     * 4. King cannot castle through check
     * 5. King can castle both sides
     * 6. Pieces can capture opponent pieces
     * 7. Pieces cannot move through pieces (except knight, duh)
     * 8. 
     * 
     */

    /* TODO
     *  1. Change 'hasMoved' to a Move() function that checks CanMoveToSpace first
     *  2. Implement en passant
     *  3. Implement changing turns
     *  4. Force King to not be in check at end of turn
     *  5. Check for stalemate
     *  6. Check for checkmate
     *  7. Implement pawn promotion
     *  8. Prevent pieces from moving if they would put your king in check (covered by reverting moves / returning false if king is in check at the end of the turn)
     *  9. 
     *  
    */
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Welcome to Chess!");
            Board.InitBoard();

            //System.ConsoleKey playing = ConsoleKey.Y;
            while (true)
            {
                //Board.FindAllSpacesAttacked();
                // if input is 'A' -> translate to 7 for X value
                // if input is '1' -> translate to 0 for Y value (may need to swap these?)
                Console.WriteLine();
                Console.Write("Enter Letter for Piece to be moved (A-H):");
                int stLong = Board.NotationToInt(Console.ReadLine());
                if(!(stLong >= 0 && stLong <= 7))
                {
                    Console.WriteLine("Please enter a letter A-H");
                }

                Console.Write("Enter Number for Piece to be moved (1-8):");
                int stLat = Board.NotationToInt(Console.ReadLine());
                if (!(stLat >= 0 && stLat <= 7))
                {
                    Console.WriteLine("Please enter a number 1-8");
                }

                Piece selectedPiece = Board.spaces[stLat][stLong].Piece;

                //TODO: show all spaces selected piece can move to, or capture

                //Console.WriteLine("Piece info: ");
                //Console.WriteLine(selectedPiece.Name + " on space " + stLat + " " + stLong + " belongs to player " + selectedPiece.belongsToPlayer);
                //Console.WriteLine("Space info: \nIs Under Attack by White: " + Board.spaces[stLat][stLong].IsUnderAttackByWhite + "\n Is Under Attack by Black: " + Board.spaces[stLat][stLong].IsUnderAttackByBlack);

                Console.Write("Enter Letter for Space to be moved to (A-H):");
                int enLong = Board.NotationToInt(Console.ReadLine());
                if (!(enLong >= 0 && enLong <= 7))
                {
                    Console.WriteLine("Please enter a letter A-H");
                }

                Console.Write("Enter Number for Space to be moved to (1-8):");
                int enLat = Board.NotationToInt(Console.ReadLine());
                if (!(enLat >= 0 && enLat <= 7))
                {
                    Console.WriteLine("Please enter a number 1-8");
                }

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

                //Console.Write("Keep playing?  Y or N:");
                //playing = Console.ReadKey().Key;
            }
        }
    }
}