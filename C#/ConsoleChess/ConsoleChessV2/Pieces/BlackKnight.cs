namespace ConsoleChessV2.Pieces
{
    internal class BlackKnight : Piece
    {
        public BlackKnight()
        {
            Name = "[n]";
            PointValue = 3;
            BelongsTo = Player.Black;
        }
        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
