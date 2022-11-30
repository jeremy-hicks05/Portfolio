namespace ConsoleChessV2
{
    internal class Piece
    {
        public string? Name { get; set; }
        public int PointValue { get; set; }
        public Player? BelongsTo { get; set; }
        public List<Space>? spacesThisPieceCanMoveTo = new();

        public Piece()
        {
            Name = "[ ]";
            PointValue = 0;
            BelongsTo = null;
        }

        public virtual void CreateListOfPiecesToInspect(Space fromSpace, Space toSpace)
        {

        }

        public virtual bool CanCaptureOrMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }

        public virtual bool IsBlocked(Space fromSpace, Space toSpace)
        {
            foreach (Space s in fromSpace.Piece?.spacesThisPieceCanMoveTo!)
            {
                //Console.WriteLine($"{s} on space {s.Column}{s.Row}");
                if (s != fromSpace.Piece.spacesThisPieceCanMoveTo.Last())
                {
                    if (s.Piece?.BelongsTo != null)
                    {
                        // piece is blocked
                        //Console.WriteLine("Piece is blocked");
                        return true;
                    }
                }
                else if (s == fromSpace.Piece.spacesThisPieceCanMoveTo.Last() &&
                    fromSpace.Piece.BelongsTo != toSpace.Piece?.BelongsTo)
                {
                    // piece is not blocked
                    //Console.WriteLine("Piece is not blocked");
                    return false;

                }
            }
            return true;
        }

        public virtual bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }

        public virtual bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }

        public virtual bool CanTryToAttackSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
