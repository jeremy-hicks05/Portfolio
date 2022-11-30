namespace ConsoleChessV2.Pieces
{
    using static Notation;
    internal class WhiteKing : Piece
    {
        public WhiteKing()
        {
            Name = "[K]";
            PointValue = 99;
            BelongsTo = Player.White;
        }
        public override bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (fromSpace == toSpace)
            {
                return false;
            }
            if ((Math.Abs(fromSpace.Column - toSpace.Column) <= 1 &&
                Math.Abs(fromSpace.Row - toSpace.Row) <= 1))
            {
                return true;
            }
            if (HasMoved == false &&
                fromSpace.Row == toSpace.Row &&
                fromSpace.Column == C["E"] && toSpace.Column == C["G"])
            {
                // attemping to castle King Side
                return true;
            }

            if (HasMoved == false &&
                fromSpace.Row == toSpace.Row &&
                fromSpace.Column == C["E"] && toSpace.Column == C["C"])
            {
                // attemping to castle Queen Side
                return true;
            }

            return false;
        }
        public override void CreateListOfPiecesToInspect(Space fromSpace, Space toSpace)
        {
            spacesToMoveToReview?.Clear();

            // if non-castling move
            if (fromSpace.Column - 1 >= 0)
            {
                // attacking left
                if (fromSpace.Column - 1 == toSpace.Column &&
                    fromSpace.Row == toSpace.Row)
                {
                    spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Column - 1][fromSpace.Row]);
                }
            }
            if (fromSpace.Row - 1 >= 0)
            {
                // attacking down
                if (fromSpace.Row - 1 == toSpace.Row &&
                    fromSpace.Column == toSpace.Column)
                {
                    spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Column][fromSpace.Row - 1]);
                }
            }
            if (fromSpace.Row + 1 <= 7)
            {
                // attacking up
                if (fromSpace.Row + 1 == toSpace.Row &&
                    fromSpace.Column == toSpace.Column)
                {
                    spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Column][fromSpace.Row + 1]);
                }
            }
            if (fromSpace.Column + 1 <= 7)
            {
                // attacking right
                if (fromSpace.Column + 1 == toSpace.Column &&
                    fromSpace.Row == toSpace.Row)
                {
                    spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Column + 1][fromSpace.Row]);
                }
            }
            if (fromSpace.Row + 1 <= 7 && fromSpace.Column + 1 <= 7)
            {
                // attacking up and right
                if (fromSpace.Row + 1 == toSpace.Row &&
                    fromSpace.Column + 1 == toSpace.Column)
                {
                    spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Column + 1][fromSpace.Row + 1]);
                }
            }
            if (fromSpace.Row + 1 <= 7 && fromSpace.Column - 1 >= 0)
            {
                // attacking up and left
                if (fromSpace.Row + 1 == toSpace.Row &&
                    fromSpace.Column - 1 == toSpace.Column)
                {
                    spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Column - 1][fromSpace.Row + 1]);
                }
            }
            if (fromSpace.Row - 1 >= 0 && fromSpace.Column + 1 <= 7)
            {
                // attacking down and right
                if (fromSpace.Row - 1 == toSpace.Row &&
                    fromSpace.Column + 1 == toSpace.Column)
                {
                    spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Column + 1][fromSpace.Row - 1]);
                }
            }
            if (fromSpace.Column - 1 >= 0 && fromSpace.Row - 1 >= 0)
            {
                // attacking down and left
                if(fromSpace.Row - 1 == toSpace.Row &&
                    fromSpace.Column - 1 == toSpace.Column)
                spacesToMoveToReview?.Add(ChessBoard.Spaces![fromSpace.Column - 1][fromSpace.Row - 1]);
            }
            // if castling King side
            if (HasMoved == false &&
                ChessBoard.Spaces![C["H"]][R["1"]].Piece?.HasMoved == false &&
                fromSpace.Row == toSpace.Row &&
                fromSpace.Column == C["E"] && toSpace.Column == C["G"])
            {
                spacesToMoveToReview?.Add(ChessBoard.Spaces![C["F"]][R["1"]]);
                spacesToMoveToReview?.Add(ChessBoard.Spaces![C["G"]][R["1"]]);
            }

            // if castling Queen side
            if (HasMoved == false &&
                ChessBoard.Spaces![C["A"]][R["1"]].Piece?.HasMoved == false &&
                fromSpace.Row == toSpace.Row &&
                fromSpace.Column == C["E"] && toSpace.Column == C["C"])
            {
                spacesToMoveToReview?.Add(ChessBoard.Spaces![C["B"]][R["1"]]);
                spacesToMoveToReview?.Add(ChessBoard.Spaces![C["C"]][R["1"]]);
                spacesToMoveToReview?.Add(ChessBoard.Spaces![C["D"]][R["1"]]);
            }
        }
    }
}
