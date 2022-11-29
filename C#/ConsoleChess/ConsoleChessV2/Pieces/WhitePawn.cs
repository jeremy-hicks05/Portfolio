namespace ConsoleChessV2.Pieces
{
    internal class WhitePawn : Piece
    {
        public WhitePawn()
        {
            Name = "[P]";
            PointValue = 1;
            BelongsTo = Player.White;
        }
        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
