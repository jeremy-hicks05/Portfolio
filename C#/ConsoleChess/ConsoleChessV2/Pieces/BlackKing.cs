namespace ConsoleChessV2.Pieces
{
    internal class BlackKing : Piece
    {
        public BlackKing()
        {
            Name = "[k]";
            PointValue = 99;
            BelongsTo = Player.Black;
        }

        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
