namespace ConsoleChessV3.Moves
{
    using ConsoleChessV3.SuperClasses;

    internal class EnPassant : ChessMove
    {
        public EnPassant(Space startingSpace, Space endingSpace) : base(startingSpace, endingSpace)
        {
            StartingSpace = startingSpace;
            TargetSpace = endingSpace;
            // TODO: add logic to ID the CapturedSpace

            //CapturedSpace = piece to the left/right of pawn
            CapturedSpace = 
                StartingPiece.GetBelongsTo() == Enums.Player.White ?
                ChessBoard.Spaces[TargetSpace.Column][TargetSpace.Row - 1] : // If WhitePawn
                ChessBoard.Spaces[TargetSpace.Column][TargetSpace.Row + 1];  // If BlackPawn


            CapturedPiece = CapturedSpace.Piece;
        }

        public override void Perform()
        {
            //TODO: Insert code to perform an EnPassant capture
            //StartingSpace.Piece.EnPassant(StartingSpace, TargetSpace);
            TargetSpace.Piece = StartingPiece;
            StartingSpace.Clear();
            CapturedPiece = CapturedSpace.Piece;
            CapturedSpace.Clear();
        }
    }
}
