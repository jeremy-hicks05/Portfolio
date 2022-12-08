namespace ConsoleChessV3.Moves
{
    using ConsoleChessV3.SuperClasses;
    using static ConsoleChessV3.Enums.Notation;
    internal class Castle : ChessMove
    {
        public Castle(Space startingSpace, Space endingSpace) : base(startingSpace, endingSpace)
        {
            // add designation for 'captured' / 'affected' piece(s)?
            if (StartingSpace.Column + 2 == TargetSpace.Column) // king side castle
            {
                RestorePiece = ChessBoard.Spaces[C["H"]][StartingSpace.Row].Piece;
                RestoreSpace = ChessBoard.Spaces[C["H"]][StartingSpace.Row];
            }
            else // queen side castle
            {
                RestorePiece = ChessBoard.Spaces[C["A"]][StartingSpace.Row].Piece;
                RestoreSpace = ChessBoard.Spaces[C["A"]][StartingSpace.Row];
            }
        }

        public override void Perform()
        {
            if (StartingSpace.Column + 2 == TargetSpace.Column) // king side castle
            {
                ChessBoard.Spaces[C["G"]][StartingSpace.Row].Piece = ChessBoard.Spaces[C["E"]][StartingSpace.Row].Piece;
                ChessBoard.Spaces[C["F"]][StartingSpace.Row].Piece = ChessBoard.Spaces[C["H"]][StartingSpace.Row].Piece;

                ChessBoard.Spaces[C["E"]][StartingSpace.Row].Clear();
                ChessBoard.Spaces[C["H"]][StartingSpace.Row].Clear();

            }
            else // Queen side castle
            {
                ChessBoard.Spaces[C["C"]][StartingSpace.Row].Piece = ChessBoard.Spaces[C["E"]][StartingSpace.Row].Piece;
                ChessBoard.Spaces[C["D"]][StartingSpace.Row].Piece = ChessBoard.Spaces[C["A"]][StartingSpace.Row].Piece;

                ChessBoard.Spaces[C["E"]][StartingSpace.Row].Clear();
                ChessBoard.Spaces[C["A"]][StartingSpace.Row].Clear();
            }
        }

        public override bool IsValidChessMove()
        {
            if (StartingSpace.Column + 2 == TargetSpace.Column) // king side castle
            {
                return !(ChessBoard.Spaces[C["E"]][StartingSpace.Row].IsUnderAttackByOpponent() ||
                    ChessBoard.Spaces[C["F"]][StartingSpace.Row].IsUnderAttackByOpponent() ||
                    ChessBoard.Spaces[C["F"]][StartingSpace.Row].IsUnderAttackByOpponent());
            }
            else // queen side castle
            {
                return !(ChessBoard.Spaces[C["E"]][StartingSpace.Row].IsUnderAttackByOpponent() ||
                    ChessBoard.Spaces[C["D"]][StartingSpace.Row].IsUnderAttackByOpponent() ||
                    ChessBoard.Spaces[C["C"]][StartingSpace.Row].IsUnderAttackByOpponent());
            }
            return false;
        }

        public override void Reverse()
        {
            StartingSpace.Piece = StartingPiece;
            StartingSpace.Piece.SetHasMoved(StartingPieceHasMoved);

            RestoreSpace.Piece = RestorePiece;
            RestoreSpace.Piece.SetHasMoved(RestorePieceHasMoved);

            if(StartingSpace.Column + 2 == TargetSpace.Column)
            {
                ChessBoard.Spaces[StartingSpace.Column + 1][StartingSpace.Row].Clear();
                ChessBoard.Spaces[StartingSpace.Column + 2][StartingSpace.Row].Clear();
            }
            else
            {
                ChessBoard.Spaces[StartingSpace.Column - 1][StartingSpace.Row].Clear();
                ChessBoard.Spaces[StartingSpace.Column - 2][StartingSpace.Row].Clear();
            }
        }
    }
}
