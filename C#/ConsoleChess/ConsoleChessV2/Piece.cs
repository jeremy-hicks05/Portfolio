using ConsoleChessV2.Pieces;

namespace ConsoleChessV2
{
    internal class Piece
    {
        public string? Name { get; set; }
        public int PointValue { get; set; }
        public Player? BelongsTo { get; set; }
        public List<Space>? spacesToMoveToReview = new();
        public List<Space>? spacesToCaptureReview = new();
        public bool HasMoved { get; set; }

        public Piece()
        {
            Name = "[ ]";
            PointValue = 0;
            BelongsTo = null;
        }

        public virtual bool IsPiecesTurn()
        {
            return ChessBoard.turn == BelongsTo;
        }

        public virtual void CreateListOfPiecesToInspect(Space fromSpace, Space toSpace)
        {
            //Console.WriteLine("Calling wrong method!");
        }

        public virtual bool CanCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // we already know move selection is legal and piece is not blocked
            return toSpace.Piece?.BelongsTo != null &&
                fromSpace.Piece?.BelongsTo != toSpace.Piece?.BelongsTo;
        }

        public virtual bool CanMoveFromSpaceToEmptySpace(Space fromSpace, Space toSpace)
        {
            return toSpace.Piece?.BelongsTo == null;
        }

        public virtual bool TryMove(Space fromSpace, Space toSpace)
        {
            Piece? tempFromSpacePiece = fromSpace.Piece;
            Piece? tempToSpacePiece = toSpace.Piece;

            toSpace.Piece = fromSpace.Piece;
            fromSpace.Clear();
            ChessBoard.FindAllSpacesAttacked();

            // verify your king is not in check
            if (ChessBoard.turn == Player.White && ChessBoard.WhiteKingSpace!.IsUnderAttackByBlack)
            {
                // cancel move
                fromSpace.Piece = tempFromSpacePiece;
                toSpace.Piece = tempToSpacePiece;
                return false;
            }
            else if (ChessBoard.turn == Player.Black && ChessBoard.BlackKingSpace!.IsUnderAttackByWhite)
            {
                // cancel move
                fromSpace.Piece = tempFromSpacePiece;
                toSpace.Piece = tempToSpacePiece;
                return false;
            }
            fromSpace.Piece = tempFromSpacePiece;
            toSpace.Piece = tempToSpacePiece;
            return true;
        }

        public virtual void Move(Space fromSpace, Space toSpace)
        {
            toSpace.Piece = fromSpace.Piece;
            toSpace.Piece!.HasMoved = true;
            fromSpace.Clear();
        }

        public virtual bool TryCapture(Space fromSpace, Space toSpace)
        {
            Piece? tempFromSpacePiece = fromSpace.Piece;
            Piece? tempToSpacePiece = toSpace.Piece;

            toSpace.Piece = fromSpace.Piece;
            fromSpace.Clear();
            toSpace.Clear();
            ChessBoard.FindAllSpacesAttacked();

            // verify your king is not in check
            if (ChessBoard.turn == Player.White && ChessBoard.WhiteKingSpace!.IsUnderAttackByBlack)
            {
                // cancel move
                fromSpace.Piece = tempFromSpacePiece;
                toSpace.Piece = tempToSpacePiece;
                return false;
            }
            else if (ChessBoard.turn == Player.Black && ChessBoard.BlackKingSpace!.IsUnderAttackByWhite)
            {
                // cancel move
                fromSpace.Piece = tempFromSpacePiece;
                toSpace.Piece = tempToSpacePiece;
                return false;
            }
            fromSpace.Piece = tempFromSpacePiece;
            toSpace.Piece = tempToSpacePiece;
            return true;
        }

        public virtual bool IsBlocked(Space fromSpace, Space toSpace)
        {
            fromSpace.Piece?.CreateListOfPiecesToInspect(fromSpace, toSpace); // added
            // move options
            foreach (Space s in fromSpace.Piece?.spacesToMoveToReview!)
            {
                if (s != fromSpace.Piece.spacesToMoveToReview.Last())
                {
                    if (s.Piece?.BelongsTo != null)
                    {
                        // piece is blocked
                        return true;
                    }
                }
                if (s == fromSpace.Piece.spacesToMoveToReview.Last())
                {
                    // piece is not blocked
                    return false;
                }
            }

            // capture options
            foreach (Space s in fromSpace.Piece?.spacesToCaptureReview!)
            {
                if (s != fromSpace.Piece.spacesToCaptureReview.Last())
                {
                    if (s.Piece?.BelongsTo != null)
                    {
                        // piece is blocked
                        return true;
                    }
                }
                if (s == fromSpace.Piece.spacesToCaptureReview.Last())
                {
                    // piece is not blocked
                    return false;
                }
            }
            return true;
        }

        public virtual bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            //Console.WriteLine("Calling wrong method!");
            return false;
        }

        public virtual bool CanLegallyTryToCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return CanLegallyTryToMoveFromSpaceToSpace(fromSpace, toSpace) &&
                    !(fromSpace.Piece!.IsBlocked(fromSpace, toSpace));
        }
    }
}
