namespace ConsoleChessV2.Pieces
{
    internal class WhiteBishop : Piece
    {
        public WhiteBishop()
        {
            Name = "[B]";
            PointValue = 3;
            BelongsTo = Player.White;
        }
        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (Math.Abs(fromSpace.Column - toSpace.Column) / Math.Abs(fromSpace.Row - toSpace.Row) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
