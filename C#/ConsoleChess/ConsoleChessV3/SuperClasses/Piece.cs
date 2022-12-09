namespace ConsoleChessV3.SuperClasses
{
    using ConsoleChessV3.Enums;
    using ConsoleChessV3.Interfaces;
    internal class Piece : IPiece
    {
        public string Name { get; set; } = $" ";
        public bool HasMoved { get; set; }
        public int PointValue { get; set; }
        public Player BelongsTo { get; set; }
        public List<Space> SpacesToReview = new();

        public virtual void BuildListOfSpacesToInspect(Space fromSpace, Space toSpace)
        {
            // default function implementation - may not use
            throw new NotImplementedException();
            //SpacesToReview.Clear();
        }

        public virtual bool CanLegallyTryToCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return toSpace.Piece is not null &&
                BelongsTo != toSpace.Piece.GetBelongsTo() &&
                CanLegallyTryToMoveFromSpaceToSpace(fromSpace, toSpace) &&
                !IsBlocked(fromSpace, toSpace);
        }

        public virtual bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // default method
            //return false;
            throw new NotImplementedException();
        }

        public virtual bool IsBlocked(Space fromSpace, Space toSpace)
        {
            BuildListOfSpacesToInspect(fromSpace, toSpace);
            foreach (Space s in SpacesToReview)
            {
                if (s != SpacesToReview.Last())
                {
                    if (s.IsOccupied())
                    {
                        // piece is blocked
                        return true;
                    }
                }
                if (s == SpacesToReview.Last())
                {
                    // piece is not blocked
                    return false;
                }
            }
            return true;
        }

        public virtual bool TryCapture(Space fromSpace, Space toSpace)
        {

            IPiece? tempFromSpacePiece = fromSpace.Piece;
            IPiece? tempToSpacePiece = toSpace.Piece;

            toSpace.Piece = fromSpace.Piece;
            fromSpace.Clear();

            // verify your king is not in check
            if (ChessBoard.KingIsInCheck())
            {
                // cancel move
                fromSpace.Piece = tempFromSpacePiece;
                toSpace.Piece = tempToSpacePiece;
                return false;
            }
            // revert move and let calling function finish it
            fromSpace.Piece = tempFromSpacePiece;
            toSpace.Piece = tempToSpacePiece;
            return true;
        }

        public virtual void Capture(Space fromSpace, Space toSpace)
        {
            toSpace.Piece = fromSpace.Piece;
            fromSpace.Clear();
        }

        public virtual bool TryMove(Space fromSpace, Space toSpace)
        {
            IPiece? fromSpacePiece = fromSpace.Piece;
            IPiece? toSpacePiece = toSpace.Piece;
            if (fromSpace.Piece is not null)
            {
                toSpace.Piece = fromSpace.Piece;
                fromSpace.Clear();
            }

            if (ChessBoard.KingIsInCheck())
            {
                // undo move
                fromSpace.Piece = fromSpacePiece;
                toSpace.Piece = toSpacePiece;
                return false;
            }
            // undo move
            fromSpace.Piece = fromSpacePiece;
            toSpace.Piece = toSpacePiece;
            return true;
        }

        public virtual void Move(Space fromSpace, Space toSpace)
        {
            if (fromSpace.Piece is not null)
            {
                fromSpace.Piece.SetHasMoved(true);
                toSpace.Piece = fromSpace.Piece;
                fromSpace.Clear();
            }
        }

        public virtual void UndoMove(ChessMove move)
        {
            if (move is not null)
            {
                move.TargetSpace.Clear();
                if (move.RestoreSpace is not null)
                {
                    move.RestoreSpace.Piece = move.RestorePiece;
                }
                move.StartingSpace.Piece = move.StartingPiece;
            }
        }

        //--- getters and setters ---//
        public int GetPointValue()
        {
            return PointValue;
        }

        public Player GetBelongsTo()
        {
            return BelongsTo;
        }

        public bool GetHasMoved()
        {
            return HasMoved;
        }

        public void SetHasMoved(bool moved)
        {
            HasMoved = moved;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
