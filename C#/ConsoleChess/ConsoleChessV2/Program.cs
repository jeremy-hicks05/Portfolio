// when you select a piece, find all the spaces it can move to
// then just see if the destination space is in that list


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

                Space startingSpace = ChessBoard.UserSelectsSpace();
                Space endingSpace = ChessBoard.UserSelectsSpace();

                startingSpace.PrintInfo();
                endingSpace.PrintInfo();

                if(startingSpace.Piece!.CanLegallyTryToMoveFromSpaceToSpace(startingSpace, endingSpace)
                    ||
                    startingSpace.Piece!.CanLegallyTryToCaptureFromSpaceToSpace(startingSpace, endingSpace))
                {
                    Console.WriteLine($"This move attempt follows {startingSpace.Piece.Name} movement rules!");
                    Console.ReadLine();

                    startingSpace.Piece.CreateListOfPiecesToInspect(startingSpace, endingSpace);

                    if(startingSpace.Piece.IsBlocked(startingSpace, endingSpace))
                    {
                        Console.WriteLine("Piece is blocked");
                    }
                    else
                    {
                        Console.WriteLine("Piece is not blocked");
                        if(startingSpace.Piece.CanCaptureFromSpaceToSpace(startingSpace, endingSpace))
                        {
                            Console.WriteLine("Piece can capture space.");
                            ChessBoard.Move(startingSpace, endingSpace);
                        }
                        else
                        {
                            Console.WriteLine("Piece cannot capture space.");
                        }
                        if(startingSpace.Piece.CanMoveFromSpaceToSpace(startingSpace, endingSpace))
                        {
                            Console.WriteLine("Piece can move from space to space?");
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