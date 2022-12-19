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
            if (fromSpace.GetPiece() is not null)
            {
                // check for White EnPassant move
                if (ChessBoard.Turn == Enums.Player.White &&
                    fromSpace.GetPiece() is Pawn &&
                    toSpace.GetPiece() is null &&
                    (Math.Abs(toSpace.Column - fromSpace.Column) == 1))
                {
                    if (ChessBoard.GetSpace(toSpace.Column, toSpace.Row - 1).GetPiece() is BlackPawn)
                    {
                        Pawn? tempPawn = ChessBoard.GetSpace(toSpace.Column, toSpace.Row - 1).GetPiece() as Pawn;
                        if (tempPawn is not null && tempPawn.HasJustMovedTwo)
                        {
                            if (ChessBoard.GetSpace(toSpace.Column, toSpace.Row - 1).GetPiece()!.GetBelongsTo() != fromSpace.GetPiece()!.GetBelongsTo())
                            {
                                return new EnPassant(fromSpace, toSpace);
                            }
                        }
                    }
                }
                // check for Black EnPassant move
                else if (ChessBoard.Turn == Enums.Player.Black &&
                    fromSpace.GetPiece() is Pawn &&
                    toSpace.GetPiece() is null &&
                    (Math.Abs(toSpace.Column - fromSpace.Column) == 1))
                {
                    if (ChessBoard.GetSpace(toSpace.Column, toSpace.Row + 1).GetPiece() is WhitePawn)
                    {
                        Pawn? tempPawn = ChessBoard.GetSpace(toSpace.Column, toSpace.Row + 1).GetPiece() as Pawn;
                        if (tempPawn is not null && tempPawn.HasJustMovedTwo)
                        {
                            if (ChessBoard.GetSpace(toSpace.Column, toSpace.Row + 1).GetPiece()!.GetBelongsTo() != fromSpace.GetPiece()!.GetBelongsTo())
                            {
                                return new EnPassant(fromSpace, toSpace);
                            }
                        }
                    }
                }
                // promotion
                else if ((fromSpace.GetPiece() is Pawn && toSpace.Row == 7) ||
                    (fromSpace.GetPiece() is Pawn && toSpace.Row == 0))
                {
                    return new Promotion(fromSpace, toSpace);
                }
                // castle
                else if (fromSpace.GetPiece() is King &&
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
