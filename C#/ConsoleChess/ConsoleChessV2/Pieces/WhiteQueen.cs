namespace ConsoleChessV2.Pieces
{
    internal class WhiteQueen : Piece
    {
        public WhiteQueen()
        {
            Name = "[Q]";
            PointValue = 9;
            BelongsTo = Player.White;
        }
        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
