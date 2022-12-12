namespace ConsoleChessV3.ChessMoves
{
    using ConsoleChessV3.Interfaces;

    internal class ChessMove : IChessMove
    {
        public Space StartingSpace;
        public IPiece StartingPiece;
        public bool StartingPieceHasMoved;

        public Space TargetSpace;
        public IPiece? TargetPiece;
        public bool TargetPieceHasMoved;

        public Space? RestoreSpace;
        public IPiece? RestorePiece;
        public bool RestorePieceHasMoved;


        public ChessMove(Space startingSpace, Space endingSpace)
        {
            StartingSpace = startingSpace;
            StartingPiece = startingSpace.Piece!;
            StartingPieceHasMoved = StartingPiece.GetHasMoved();

            TargetSpace = endingSpace;
            if (TargetSpace.Piece is not null)
            {
                TargetPiece = endingSpace.Piece!;
                TargetPieceHasMoved = TargetPiece.GetHasMoved();
            }
        }

        public virtual void Perform()
        {
            // must be overridden by subclasses
            throw new NotImplementedException();
        }

        public virtual void Reverse()
        {
            // must be overridden by subclasses
            throw new NotImplementedException();
        }

        public virtual bool IsValidChessMove()
        {
            if (StartingSpace is not null && StartingSpace.Piece is not null)
            {
                if (StartingSpace.Piece.CanLegallyTryToMoveFromSpaceToSpace(StartingSpace, TargetSpace))
                {
                    if (!StartingSpace.Piece.IsBlocked(StartingSpace, TargetSpace))
                    {

                        if (StartingSpace.Piece.TryMove(StartingSpace, TargetSpace))
                        {
                            
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("King would be in check");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Piece is blocked");
                        Console.ReadLine();
                    }
                }
                else if (StartingSpace.Piece.CanLegallyTryToCaptureFromSpaceToSpace(StartingSpace, TargetSpace))
                {
                    if (!StartingSpace.Piece.IsBlocked(StartingSpace, TargetSpace))
                    {
                        if (StartingSpace.Piece.TryCapture(StartingSpace, TargetSpace))
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("King would be in check");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Piece is blocked");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Piece does not move like that");
                    Console.ReadLine();
                }
            }
            return false;
        }
    }
}
