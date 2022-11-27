/************************************************
 *                                              *
 * Chess - By Jeremy Hicks (c) 2022             *
 * Tested by - Kari Seitz                       *
 * Early Review(s) - Shaun Lake                 *
 *                                              *
 ************************************************/


namespace ConsoleChess
{
    using ConsoleChess.Interfaces;
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
     *  9. Refactor, compress, and condense code using functions
     *  10. Fix rook - it cannot move more than 1 space at a time
     *  11. Check input inside while loops to ensure user only enters A-H and 1-8
     *  12. Allow resignation
     *  13. Show all available moves for selected piece
    */
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Welcome to Chess!");
            Board.InitBoard();

            //System.ConsoleKey playing = ConsoleKey.Y;
            while (true) // White is not in checkmate/stalemate and Black is not in checkmate/stalemate and neither has resigned
            {
                int startLongitude = -1;
                
                // if input is 'A' -> translate to 7 for X value
                // if input is '1' -> translate to 0 for Y value
                Console.WriteLine();
                while (!(startLongitude >= 0 && startLongitude <= 7))
                {
                    Console.Write("Enter Letter for Piece to be moved (A-H):");
                    startLongitude = Board.NotationToInt(Console.ReadLine());
                    if (!(startLongitude >= 0 && startLongitude <= 7))
                    {
                        Console.WriteLine("Please enter a letter A-H");
                    }
                }

                int startLatitude = -1;                
                
                while (!(startLatitude >= 0 && startLatitude <= 7))
                {
                    Console.Write("Enter Number for Piece to be moved (1-8):");
                    startLatitude = Board.NotationToInt(Console.ReadLine());
                    if (!(startLatitude >= 0 && startLatitude <= 7))
                    {
                        Console.WriteLine("Please enter a number 1-8");
                    }
                }

                Piece selectedPiece = Board.spaces[startLatitude][startLongitude].Piece;

                //TODO: show all spaces selected piece can move to, or capture


                //Console.WriteLine("Piece info: ");
                //Console.WriteLine(selectedPiece.Name + " on space " + stLat + " " + stLong + " belongs to player " + selectedPiece.belongsToPlayer);
                //Console.WriteLine("Space info: \nIs Under Attack by White: " + Board.spaces[stLat][stLong].IsUnderAttackByWhite + "\n Is Under Attack by Black: " + Board.spaces[stLat][stLong].IsUnderAttackByBlack);

                int endLongitude = -1;

                while (!(endLongitude >= 0 && endLongitude <= 7))
                {
                    Console.Write("Enter Letter for Space to be moved to (A-H):");
                    endLongitude = Board.NotationToInt(Console.ReadLine());
                    if (!(endLongitude >= 0 && endLongitude <= 7))
                    {
                        Console.WriteLine("Please enter a letter A-H");
                    }
                }

                int endLatitude = -1;
                while (!(endLatitude >= 0 && endLatitude <= 7))
                {
                    Console.Write("Enter Number for Space to be moved to (1-8):");
                    endLatitude = Board.NotationToInt(Console.ReadLine());
                    if (!(endLatitude >= 0 && endLatitude <= 7))
                    {
                        Console.WriteLine("Please enter a number 1-8");
                    }
                }
                Space startingSpace = Board.spaces[startLatitude][startLongitude];
                Space destinationSpace = Board.spaces[endLatitude][endLongitude];

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