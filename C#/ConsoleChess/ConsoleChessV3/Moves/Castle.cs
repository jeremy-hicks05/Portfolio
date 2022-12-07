namespace ConsoleChessV3.Moves
{
    using ConsoleChessV3.SuperClasses;
    using static ConsoleChessV3.Enums.Notation;
    internal class Castle : ChessMove
    {
        public Castle(Space startingSpace, Space endingSpace) : base(startingSpace, endingSpace)
        {
            // add designation for 'captured' / 'affected' piece(s)?
            RestorePiece = ChessBoard.Spaces[C["H"]][StartingSpace.Row].Piece;
            RestoreSpace = ChessBoard.Spaces[C["H"]][StartingSpace.Row];
        }

        public override void Perform()
        {
            // TODO: insert code to perform a Castle
            // we know it's a King and has not moved
            if (StartingSpace.Piece is not null && 
                ChessBoard.Spaces is not null && 
                ChessBoard.Spaces[C["H"]][StartingSpace.Row].Piece is not null)
            {
                Console.WriteLine("Performing Castle!");
                StartingSpace.Piece.Move(StartingSpace, TargetSpace);
                ChessBoard.Spaces[C["H"]][StartingSpace.Row].Piece
                    .Move(ChessBoard.Spaces[C["H"]][StartingSpace.Row],
                            ChessBoard.Spaces[C["F"]][StartingSpace.Row]);
            }
        }
    }
}
