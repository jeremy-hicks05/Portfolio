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
     * 10. Implemented pawn promotion
     * 11. Pieces can now block check, and cannot move if that piece is pinned
     * 
     */

    /* TODO
     *  1. Implement en passant
     *  2. Force King to not be in check at end of turn
     *  3. Check for stalemate
     *  4. Check for checkmate
     *  5. Prevent pieces from moving if they would put your king in check (may be covered by reverting moves / returning false if king is in check at the end of the turn)
     *      5a. Need to hold 3? temporary values - put the destination piece back if 
     *          the king is in check - if it is a capture, this might capture the piece
     *          and not restore it if I am not careful
     *  6. Refactor, compress, and condense code using functions
     *  7. Allow resignation
     *  8. 
    */
    internal class Program
    {
        static void Main(string[] args)
        {
            Board.InitBoard();

            while (true) // White is not in checkmate/stalemate and
                         // Black is not in checkmate/stalemate and
                         // neither has resigned
            {

                Space startingSpace = Board.GetStartingSpace();

                //Console.WriteLine("Piece " + startingSpace.Piece + " on space " +
                //    startingSpace.X + ", " + startingSpace.Y + " selected.");

                Space destinationSpace = Board.GetDestinationSpace();

                // check piece's ability to move to selected space
                Board.MovePieceFromSpaceToSpace(
                    startingSpace.Piece.CanMoveFromSpaceToSpace(
                        startingSpace,
                        destinationSpace),
                    startingSpace, destinationSpace);

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