namespace ConsoleChessV2.Pieces
{
    internal class BlackRook : Piece
    {
        public BlackRook()
        {
            Name = "[r]";
            PointValue = 5;
            BelongsTo = Player.Black;
        }
        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
