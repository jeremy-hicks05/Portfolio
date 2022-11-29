namespace ConsoleChessV2.Pieces
{
    internal class WhiteRook : Piece
    {
        public WhiteRook()
        {
            Name = "[R]";
            PointValue = 5;
            BelongsTo = Player.White;
        }
        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
