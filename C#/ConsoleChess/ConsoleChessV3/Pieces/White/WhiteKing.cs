namespace ConsoleChessV3.Pieces.White
{
    using ConsoleChessV3.Interfaces;
    using static ConsoleChessV3.Enums.Notation;
    internal class WhiteKing : King
    {
        public WhiteKing()
        {
            Name = "K";
            BelongsTo = Enums.Player.White;
        }

        public override bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return ((Math.Abs(fromSpace.Column - toSpace.Column) <= 1 && 
                    Math.Abs(fromSpace.Row - toSpace.Row) <= 1))
                        ||
                    (fromSpace.Column == C["E"] && fromSpace.Row == R["1"] &&
                    toSpace.Column == C["G"] && toSpace.Row == R["1"]) 
                        ||
                    (fromSpace.Column == C["E"] && fromSpace.Row == R["1"] &&
                    toSpace.Column == C["C"] && toSpace.Row == R["1"]);
        }

        public override void BuildListOfSpacesToInspect(Space fromSpace, Space toSpace)
        {
            if (ChessBoard.Spaces is not null)
            {
                SpacesToReview.Clear();

                // if non-castling move
                if (fromSpace.Column - 1 >= 0)
                {
                    // attacking left
                    if (fromSpace.Column - 1 == toSpace.Column &&
                        fromSpace.Row == toSpace.Row)
                    {
                        SpacesToReview.Add(ChessBoard.Spaces[fromSpace.Column - 1][fromSpace.Row]);
                    }
                }
                if (fromSpace.Row - 1 >= 0)
                {
                    // attacking down
                    if (fromSpace.Row - 1 == toSpace.Row &&
                        fromSpace.Column == toSpace.Column)
                    {
                        SpacesToReview.Add(ChessBoard.Spaces[fromSpace.Column][fromSpace.Row - 1]);
                    }
                }
                if (fromSpace.Row + 1 <= 7)
                {
                    // attacking up
                    if (fromSpace.Row + 1 == toSpace.Row &&
                        fromSpace.Column == toSpace.Column)
                    {
                        SpacesToReview.Add(ChessBoard.Spaces[fromSpace.Column][fromSpace.Row + 1]);
                    }
                }
                if (fromSpace.Column + 1 <= 7)
                {
                    // attacking right
                    if (fromSpace.Column + 1 == toSpace.Column &&
                        fromSpace.Row == toSpace.Row)
                    {
                        SpacesToReview.Add(ChessBoard.Spaces[fromSpace.Column + 1][fromSpace.Row]);
                    }
                }
                if (fromSpace.Row + 1 <= 7 && fromSpace.Column + 1 <= 7)
                {
                    // attacking up and right
                    if (fromSpace.Row + 1 == toSpace.Row &&
                        fromSpace.Column + 1 == toSpace.Column)
                    {
                        SpacesToReview.Add(ChessBoard.Spaces[fromSpace.Column + 1][fromSpace.Row + 1]);
                    }
                }
                if (fromSpace.Row + 1 <= 7 && fromSpace.Column - 1 >= 0)
                {
                    // attacking up and left
                    if (fromSpace.Row + 1 == toSpace.Row &&
                        fromSpace.Column - 1 == toSpace.Column)
                    {
                        SpacesToReview.Add(ChessBoard.Spaces[fromSpace.Column - 1][fromSpace.Row + 1]);
                    }
                }
                if (fromSpace.Row - 1 >= 0 && fromSpace.Column + 1 <= 7)
                {
                    // attacking down and right
                    if (fromSpace.Row - 1 == toSpace.Row &&
                        fromSpace.Column + 1 == toSpace.Column)
                    {
                        SpacesToReview.Add(ChessBoard.Spaces[fromSpace.Column + 1][fromSpace.Row - 1]);
                    }
                }
                if (fromSpace.Column - 1 >= 0 && fromSpace.Row - 1 >= 0)
                {
                    // attacking down and left
                    if (fromSpace.Row - 1 == toSpace.Row &&
                        fromSpace.Column - 1 == toSpace.Column)
                    {
                        SpacesToReview.Add(ChessBoard.Spaces[fromSpace.Column - 1][fromSpace.Row - 1]);
                    }
                }
                // if castling King side
                if (HasMoved == false &&
                    fromSpace.Row == toSpace.Row &&
                    ChessBoard.Spaces[C["H"]][R["1"]].Piece?.GetHasMoved() == false &&
                    fromSpace.Column == C["E"] && toSpace.Column == C["G"])
                {
                    SpacesToReview.Add(ChessBoard.Spaces![C["F"]][R["1"]]);
                    SpacesToReview.Add(ChessBoard.Spaces![C["G"]][R["1"]]);
                }

                // if castling Queen side
                if (HasMoved == false &&
                    fromSpace.Row == toSpace.Row &&
                    ChessBoard.Spaces[C["A"]][R["1"]].Piece?.GetHasMoved() == false &&
                    fromSpace.Column == C["E"] && toSpace.Column == C["C"])
                {
                    SpacesToReview.Add(ChessBoard.Spaces[C["B"]][R["1"]]);
                    SpacesToReview.Add(ChessBoard.Spaces[C["C"]][R["1"]]);
                    SpacesToReview.Add(ChessBoard.Spaces[C["D"]][R["1"]]);
                }
            }
        }

        public override bool TryMove(Space fromSpace, Space toSpace)
        {
            IPiece? fromSpacePiece = fromSpace.Piece;
            IPiece? toSpacePiece = toSpace.Piece;
            if (fromSpace.Piece is not null)
            {
                toSpace.Piece = fromSpace.Piece;
                // test from new King position
                ChessBoard.WhiteKingSpace = toSpace;
                fromSpace.Clear();
            }

            if (ChessBoard.KingIsInCheck())
            {
                // undo move
                fromSpace.Piece = fromSpacePiece;
                toSpace.Piece = toSpacePiece;
                ChessBoard.WhiteKingSpace = fromSpace;
                return false;
            }
            // undo move
            fromSpace.Piece = fromSpacePiece;
            toSpace.Piece = toSpacePiece;
            ChessBoard.WhiteKingSpace = fromSpace;
            return true;
        }

        public override void Move(Space fromSpace, Space toSpace)
        {
            if (fromSpace.Piece is not null)
            {
                fromSpace.Piece.SetHasMoved(true);
                toSpace.Piece = fromSpace.Piece;
                ChessBoard.WhiteKingSpace = toSpace;
                fromSpace.Clear();
            }
        }

        public override bool TryCapture(Space fromSpace, Space toSpace)
        {
            if (CanLegallyTryToCaptureFromSpaceToSpace(fromSpace, toSpace) &&
                !IsBlocked(fromSpace, toSpace) &&
                toSpace.Piece?.GetBelongsTo() != fromSpace.Piece?.GetBelongsTo())
            {
                IPiece? tempFromSpacePiece = fromSpace.Piece;
                IPiece? tempToSpacePiece = toSpace.Piece;

                toSpace.Piece = fromSpace.Piece;
                ChessBoard.WhiteKingSpace = toSpace;
                fromSpace.Clear();

                // verify your king is not in check
                if (ChessBoard.KingIsInCheck())
                {
                    // cancel move
                    fromSpace.Piece = tempFromSpacePiece;
                    toSpace.Piece = tempToSpacePiece;
                    ChessBoard.WhiteKingSpace = fromSpace;
                    return false;
                }
                // revert move and let calling function finish it
                fromSpace.Piece = tempFromSpacePiece;
                toSpace.Piece = tempToSpacePiece;
                ChessBoard.WhiteKingSpace = fromSpace;
                return true;
            }
            return false;
        }
    }
}
