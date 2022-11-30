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

        public virtual void CreateListOfPiecesToInspect(Space fromSpace, Space toSpace)
        {

        }

        public virtual bool CanCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // we already know move selection is legal and piece is not blocked
            return toSpace.Piece?.BelongsTo != null &&
                fromSpace.Piece?.BelongsTo != toSpace.Piece?.BelongsTo;
        }

        public virtual bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return fromSpace.Piece!
                        .CanLegallyTryToMoveFromSpaceToSpace(fromSpace, toSpace) &&
                    toSpace.Piece?.BelongsTo == null &&
                   !(fromSpace.Piece
                        .IsBlocked(fromSpace, toSpace));
        }

        public virtual bool IsBlocked(Space fromSpace, Space toSpace)
        {
            foreach (Space s in fromSpace.Piece?.spacesToMoveToReview!)
            {
                //Console.WriteLine($"{s} on space {s.Column}{s.Row}");
                if (s != fromSpace.Piece.spacesToMoveToReview.Last())
                {
                    if (s.Piece?.BelongsTo != null)
                    {
                        // piece is blocked
                        //Console.WriteLine("Piece is blocked");
                        return true;
                    }
                }
                else if (s == fromSpace.Piece.spacesToMoveToReview.Last() &&
                    fromSpace.Piece.BelongsTo != toSpace.Piece?.BelongsTo)
                {
                    // piece is not blocked
                    //Console.WriteLine("Piece is not blocked");
                    return false;

                }
            }
            return false;
        }

        public virtual bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }

        public virtual bool CanLegallyTryToCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
