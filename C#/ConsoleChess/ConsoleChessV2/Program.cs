// when you select a piece, find all the spaces it can move to
// then just see if the destination space is in that list


namespace ConsoleChessV2
{
    using static Notation;
    internal class Program
    {
        static void Main()
        {
            // Initialize Board
            ChessBoard.InitBoard();
            ChessBoard.PrintBoard();
            while (true)
            {

                Space startingSpace = ChessBoard.UserSelectsSpace();
                Space endingSpace = ChessBoard.UserSelectsSpace();

                startingSpace.PrintInfo();
                endingSpace.PrintInfo();

                if(startingSpace.Piece!.CanLegallyTryToMoveFromSpaceToSpace(startingSpace, endingSpace))
                {
                    Console.WriteLine($"This move attempt follows {startingSpace.Piece.Name} movement rules!");
                    Console.ReadLine();
                    Console.WriteLine("This piece can move to...");

                    startingSpace.Piece.CreateListOfPiecesToInspect(startingSpace, endingSpace);
                    foreach (Space s in startingSpace.Piece.spacesThisPieceCanMoveTo!)
                    {
                        Console.WriteLine($"{s} on space {s.Column}{s.Row}");
                        if (s != startingSpace.Piece.spacesThisPieceCanMoveTo.Last())
                        {
                            if (s.Piece?.BelongsTo != null)
                            {
                                // piece is blocked
                                Console.WriteLine("Piece is blocked");
                                break;
                            }
                        }
                        else if(s == startingSpace.Piece.spacesThisPieceCanMoveTo.Last() && 
                            startingSpace.Piece.BelongsTo != endingSpace.Piece.BelongsTo)
                        {
                            // piece is not blocked
                            Console.WriteLine("Piece is not blocked");
                            ChessBoard.Move(startingSpace, endingSpace);
                        }
                    }
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"This move attempt does not follow {startingSpace.Piece.Name} movement rules!");
                    Console.ReadLine();
                }

                ChessBoard.PrintBoard();
            }
        }
    }
}