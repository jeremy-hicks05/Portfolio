namespace ConsoleChessV3.Pieces
{
    using ConsoleChessV3.Enums;
    using ConsoleChessV3.Interfaces;
    using ConsoleChessV3.ChessMoves;

    internal class Piece : IPiece
    {
        public string Name { get; set; } = $" ";
        public bool HasMoved { get; set; }
        public int PointValue { get; set; }
        public Player BelongsTo { get; set; }
        public List<Space> SpacesToReview = new();

        public virtual void BuildListOfSpacesToInspect(Space fromSpace, Space toSpace)
        {
            // must be overriden by sub class
            throw new NotImplementedException();
        }

        public virtual bool CanLegallyTryToCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return toSpace.GetPiece() is not null &&
                BelongsTo != toSpace.GetPiece()!.GetBelongsTo() &&
                CanLegallyTryToMoveFromSpaceToSpace(fromSpace, toSpace) &&
                !IsBlocked(fromSpace, toSpace);
        }

        public virtual bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // must be overriden by sub class
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

            IPiece? tempFromSpacePiece = fromSpace.GetPiece();
            IPiece? tempToSpacePiece = toSpace.GetPiece();

            toSpace.SetPiece(fromSpace.GetPiece());
            fromSpace.Clear();

            // verify your king is not in check
            if (ChessBoard.KingIsInCheck())
            {
                // cancel move
                fromSpace.SetPiece(tempFromSpacePiece);
                toSpace.SetPiece(tempToSpacePiece);
                return false;
            }
            // revert move and let calling function finish it
            fromSpace.SetPiece(tempFromSpacePiece);
            toSpace.SetPiece(tempToSpacePiece);
            return true;
        }

        public virtual void Capture(Space fromSpace, Space toSpace)
        {
            toSpace.SetPiece(fromSpace.GetPiece());
            fromSpace.Clear();
        }

        public virtual bool TryMove(Space fromSpace, Space toSpace)
        {
            IPiece? fromSpacePiece = fromSpace.GetPiece();
            IPiece? toSpacePiece = toSpace.GetPiece();
            if (fromSpace.GetPiece() is not null)
            {
                toSpace.SetPiece(fromSpace.GetPiece());
                fromSpace.Clear();
            }

            if (ChessBoard.KingIsInCheck())
            {
                // undo move
                fromSpace.SetPiece(fromSpacePiece);
                toSpace.SetPiece(toSpacePiece);
                return false;
            }
            // undo move
            fromSpace.SetPiece(fromSpacePiece);
            toSpace.SetPiece(toSpacePiece);
            return true;
        }

        public virtual void Move(Space fromSpace, Space toSpace)
        {
            if (fromSpace.GetPiece() is not null)
            {
                fromSpace.GetPiece()!.SetHasMoved(true);
                toSpace.SetPiece(fromSpace.GetPiece());
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
                    move.RestoreSpace.SetPiece(move.RestorePiece);
                }
                move.StartingSpace.SetPiece(move.StartingPiece);
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
