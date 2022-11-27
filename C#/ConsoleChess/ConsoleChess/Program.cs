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
     * 8. Inputs can only be A-H and 1-8
     * 9. Implemented changing turns
     * 10.
     * 
     */

    /* TODO
     *  1. Implement en passant
     *  2. Force King to not be in check at end of turn
     *  3. Check for stalemate
     *  4. Check for checkmate
     *  5. Implement pawn promotion
     *  6. Prevent pieces from moving if they would put your king in check (may be covered by reverting moves / returning false if king is in check at the end of the turn)
     *  7. Refactor, compress, and condense code using functions
     *  8. Allow resignation
     *  9. Show all available moves for selected piece
     *  10. Stop pawns from multiplying when moving as black
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
                // get player input for move

                //Piece selectedPiece = PlayerSelectsPiece();
                // check if Piece is owned by a player Black or White
                // may put this ^ inside PlayerSelectSpace(); below

                //Space startingSpace = PlayerSelectSpace();
                //Space destinationSpace = PlayerSelectDestination();

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
                // showPossibleMoves(selectedPiece);
                // OR
                // showPossibleMoves(startingSpace);

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

                // set space references
                Space startingSpace = Board.spaces[startLatitude][startLongitude];
                Space destinationSpace = Board.spaces[endLatitude][endLongitude];

                // check piece's ability to move to selected space
                if (selectedPiece.CanMoveFromSpaceToSpace(
                    startingSpace,
                    destinationSpace))
                {
                    // move selected piece from its space to the destination space
                    Board.MovePieceFromSpaceToSpace(startingSpace, destinationSpace);
                }

                // clear console
                Console.Clear();

                // refresh spaces' AttackedbyWhite/Black property
                Board.FindAllSpacesAttacked();

                // re-print board
                Board.PrintBoard();

                // check for stalemate / checkmate
            }
        }
    }
}