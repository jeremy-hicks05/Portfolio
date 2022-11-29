namespace ConsoleChessV2.Pieces
{
    internal class BlackBishop : Piece
    {
        public BlackBishop()
        {
            Name = "[b]";
            PointValue = 3;
            BelongsTo = Player.Black;
        }

        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if(Math.Abs(fromSpace.Column - toSpace.Column) / Math.Abs(fromSpace.Row - toSpace.Row) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
