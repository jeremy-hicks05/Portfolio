namespace ConsoleChessV2
{
    internal class Piece
    {
        public string? Name { get; set; }
        public int PointValue { get; set; }
        public Player? BelongsTo { get; set; }

        public Piece()
        {
            Name = "[ ]";
            PointValue = 0;
            BelongsTo = null;
        }

        public virtual bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
