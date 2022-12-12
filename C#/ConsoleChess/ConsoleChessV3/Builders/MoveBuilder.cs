using ConsoleChessV3.Pieces.Subclasses;
using ConsoleChessV3.ChessMoves;
using ConsoleChessV3.ChessMoves.Subclasses;
using ConsoleChessV3.Pieces.Black;
using ConsoleChessV3.Pieces.White;

namespace ConsoleChessV3.Builders
{
    internal static class MoveBuilder
    {
        /// <summary>
        /// Builds a ChessMove, determining which type it is (EnPassant, Promotion, Capture, Move, Castle)
        /// and returns it to the calling function for future performing
        /// </summary>
        /// <param name="fromSpace"></param>
        /// <param name="toSpace"></param>
        /// <returns></returns>
        public static ChessMove? Build(Space fromSpace, Space toSpace)
        {
            if (ChessBoard.Spaces is not null && fromSpace.Piece is not null)
            {
                // check for White EnPassant move
                if (ChessBoard.Turn == Enums.Player.White &&
                    fromSpace.Piece is Pawn &&
                    toSpace.Piece is null &&
                    (Math.Abs(toSpace.Column - fromSpace.Column) == 1))
                {
                    if (ChessBoard.Spaces[toSpace.Column][toSpace.Row - 1].Piece is BlackPawn)
                    {
                        Pawn? tempPawn = ChessBoard.Spaces[toSpace.Column][toSpace.Row - 1].Piece as Pawn;
                        if (tempPawn is not null && tempPawn.HasJustMovedTwo)
                        {
                            if (ChessBoard.Spaces[toSpace.Column][toSpace.Row - 1].Piece!.GetBelongsTo() != fromSpace.Piece.GetBelongsTo())
                            {
                                return new EnPassant(fromSpace, toSpace);
                            }
                        }
                    }
                }
                // check for Black EnPassant move
                else if (ChessBoard.Turn == Enums.Player.Black &&
                    fromSpace.Piece is Pawn &&
                    toSpace.Piece is null &&
                    (Math.Abs(toSpace.Column - fromSpace.Column) == 1))
                {
                    if (ChessBoard.Spaces[toSpace.Column][toSpace.Row + 1].Piece is WhitePawn)
                    {
                        Pawn? tempPawn = ChessBoard.Spaces[toSpace.Column][toSpace.Row + 1].Piece as Pawn;
                        if (tempPawn is not null && tempPawn.HasJustMovedTwo)
                        {
                            if (ChessBoard.Spaces[toSpace.Column][toSpace.Row + 1].Piece!.GetBelongsTo() != fromSpace.Piece.GetBelongsTo())
                            {
                                return new EnPassant(fromSpace, toSpace);
                            }
                        }
                    }
                }
                // promotion
                else if ((fromSpace.Piece is Pawn && toSpace.Row == 7) ||
                    (fromSpace.Piece is Pawn && toSpace.Row == 0))
                {
                    return new Promotion(fromSpace, toSpace);
                }
                // castle
                else if (fromSpace.Piece is King &&
                    Math.Abs(fromSpace.Column - toSpace.Column) == 2)
                {
                    return new Castle(fromSpace, toSpace);
                }
                // capture
                else if (toSpace.IsOccupied())
                {
                    return new Capture(fromSpace, toSpace);
                }
                // standard move
                else
                {
                    return new Move(fromSpace, toSpace);
                }
            }
            return null;
        }
    }
}
