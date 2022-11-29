namespace ConsoleChessV2.Pieces
{
    internal class BlackPawn : Piece
    {
        public BlackPawn()
        {
            Name = "[p]";
            PointValue = 1;
            BelongsTo = Player.Black;
        }
        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
