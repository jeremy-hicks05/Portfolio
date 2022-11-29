namespace ConsoleChessV2.Pieces
{
    internal class WhiteKnight : Piece
    {
        public WhiteKnight()
        {
            Name = "[N]";
            PointValue = 1;
            BelongsTo = Player.White;
        }
        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
