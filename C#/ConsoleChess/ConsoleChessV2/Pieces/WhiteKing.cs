namespace ConsoleChessV2.Pieces
{
    internal class WhiteKing : Piece
    {
        public WhiteKing()
        {
            Name = "[K]";
            PointValue = 99;
            BelongsTo = Player.White;
        }
        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
