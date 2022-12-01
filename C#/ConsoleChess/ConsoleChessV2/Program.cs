/************************************************
 * ConsoleChess V2                              *
 * by Jeremy Hicks (c) 2022                     *
 * Tested By: Kari Seitz                        *
 * Early Review - Shaun Lake                    *
 *                                              *
 *                                              *
 ************************************************/

/* Things to test:
 * 
 * 
 * 
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
            ChessBoard.PrintBoard();
            while (true)
            {
                Console.WriteLine($"-{ChessBoard.turn}'s Turn-");
                ChessBoard.FindAllSpacesAttacked();
                Space startingSpace = ChessBoard.UserSelectsSpace();
                //startingSpace.PrintInfo();

                Space endingSpace = ChessBoard.UserSelectsSpace();
                //endingSpace.PrintInfo();

                //Console.ReadLine();

                if (startingSpace.Piece?.BelongsTo == ChessBoard.turn)
                {

                    if (startingSpace.Piece!.CanLegallyTryToMoveFromSpaceToSpace(startingSpace, endingSpace)
                        ||
                        startingSpace.Piece!.CanLegallyTryToCaptureFromSpaceToSpace(startingSpace, endingSpace))
                    {
                        //Console.WriteLine($"This move attempt follows {startingSpace.Piece.Name} movement rules!");
                        //Console.ReadLine();

                        startingSpace.Piece.CreateListOfPiecesToInspect(startingSpace, endingSpace);

                        if (startingSpace.Piece.IsBlocked(startingSpace, endingSpace))
                        {
                            //Console.WriteLine("Piece is blocked");
                            //Console.ReadLine();
                        }
                        else
                        {
                            //Console.WriteLine("Piece is not blocked");
                            //Console.ReadLine();
                            if (startingSpace.Piece.CanCaptureFromSpaceToSpace(startingSpace, endingSpace))
                            {
                                //Console.WriteLine("Piece can capture space.");
                                //Console.ReadLine();
                                if (startingSpace.Piece.TryCapture(startingSpace, endingSpace))
                                {
                                    startingSpace.Piece.Move(startingSpace, endingSpace);
                                    ChessBoard.ChangeTurn();
                                }
                                else
                                {
                                    //Console.WriteLine("King is in check!");
                                    //Console.ReadLine();
                                }
                            }
                            else if (startingSpace.Piece.CanMoveFromSpaceToEmptySpace(startingSpace, endingSpace))
                            {
                                //Console.WriteLine("Piece can move from space to empty space");
                                //Console.ReadLine();
                                if (startingSpace.Piece.TryMove(startingSpace, endingSpace))
                                {
                                    startingSpace.Piece.Move(startingSpace, endingSpace);
                                    ChessBoard.ChangeTurn();
                                }
                                else
                                {
                                    //Console.WriteLine("King is in check!");
                                    //Console.ReadLine();
                                }
                            }
                            else
                            {
                                //Console.WriteLine("Piece cannot capture or move to space.");
                                //Console.ReadLine();
                            }
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"This move attempt does not follow {startingSpace.Piece.Name} movement rules!");
                        //Console.ReadLine();
                    }

                    ChessBoard.FindAllSpacesAttacked();
                }
                ChessBoard.PrintBoard();
            }
        }
    }
}