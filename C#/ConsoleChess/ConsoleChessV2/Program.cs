/************************************************
 * ConsoleChess V2                              *
 * by Jeremy Hicks (c) 2022                     *
 * Tested By: Kari Seitz                        *
 * Early Review - Shaun Lake                    *
 *                                              *
 *                                              *
 ************************************************/

/* TODO: Add stack of moves for popping to "takeback"
 * Test more games alongside lichess
 * 
 * 
 * 
 * 
 */

namespace ConsoleChessV2
{
    internal class Program
    {
        static void Main()
        {
            // Initialize Board
            ChessBoard.InitBoard();
            while (!(ChessBoard.WhiteIsCheckMated()) &&
                    !(ChessBoard.WhiteIsStaleMated()) &&
                    !(ChessBoard.BlackIsCheckMated()) &&
                    !(ChessBoard.BlackIsStaleMated()))
            {
                ChessBoard.PrintBoard();
                ChessBoard.WhiteIsCheckMated();
                ChessBoard.BlackIsCheckMated();
                ChessBoard.WhiteIsStaleMated();
                ChessBoard.BlackIsStaleMated();



                // get Starting and Ending Spaces from User Input
                Space startingSpace = ChessBoard.UserSelectsSpace();
                Space endingSpace = ChessBoard.UserSelectsSpace();

                Piece? startingPiece = startingSpace.Piece;
                Piece? endingPiece = endingSpace.Piece;

                bool startingPieceHasMoved = startingSpace.Piece!.HasMoved;
                bool endingPieceHasMoved = endingSpace.Piece!.HasMoved;

                // check if Player's turn corresponds to the starting piece's owner
                if (startingSpace.Piece?.BelongsTo == ChessBoard.turn)
                {
                    // set "IsUnderAttackBy{Player} for every piece's attacked squares
                    ChessBoard.FindAllSpacesAttacked();

                    // compare attempted move to basic rules of chess piece movement
                    if (startingSpace.Piece!.CanLegallyTryToMoveFromSpaceToSpace(startingSpace, endingSpace)
                        ||
                        // compare attempted move to basic rules of chess piece capture
                        startingSpace.Piece!.CanLegallyTryToCaptureFromSpaceToSpace(startingSpace, endingSpace))
                    {
                        // generate list of spaces between starting and ending space
                        startingSpace.Piece.CreateListOfPiecesToInspect(startingSpace, endingSpace);

                        // determine if piece is able to get to the end of the list without hitting another piece
                        if (!(startingSpace.Piece.IsBlocked(startingSpace, endingSpace)))
                        {
                            // determine if piece can capture ending space
                            if (startingSpace.Piece.CanCaptureFromSpaceToSpace(startingSpace, endingSpace))
                            {
                                // try capturing the piece on the ending space, test if rules are followed (not in check)
                                if (startingSpace.Piece.TryCapture(startingSpace, endingSpace))
                                {
                                    // finally move piece to destination
                                    startingSpace.Piece.Move(startingSpace, endingSpace);
                                    ChessBoard.MovesPlayed.Push((startingSpace, endingSpace, endingPiece, startingPieceHasMoved)!);
                                    //ChessBoard.ListMovesPlayed();
                                    Console.ReadLine();
                                    ChessBoard.ChangeTurn();
                                }
                            }
                            // determine if piece can move to an empty space (not capture)
                            else if (startingSpace.Piece.CanMoveFromSpaceToEmptySpace(startingSpace, endingSpace))
                            {
                                // try moving the piece on the starting space, test if rules are followed (not in check)
                                if (startingSpace.Piece.TryMove(startingSpace, endingSpace))
                                {
                                    // finally move piece to destination
                                    startingSpace.Piece.Move(startingSpace, endingSpace);
                                    ChessBoard.MovesPlayed.Push((startingSpace, endingSpace, endingPiece, startingPieceHasMoved)!);
                                    //ChessBoard.ListMovesPlayed();
                                    Console.ReadLine();
                                    ChessBoard.ChangeTurn();
                                }
                            }
                        }
                    }
                }
                ChessBoard.PrintBoard();
            }
        }
    }
}