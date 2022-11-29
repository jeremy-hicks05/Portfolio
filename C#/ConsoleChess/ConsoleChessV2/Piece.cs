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
