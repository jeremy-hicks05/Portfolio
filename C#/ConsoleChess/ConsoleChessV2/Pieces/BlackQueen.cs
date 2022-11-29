namespace ConsoleChessV2.Pieces
{
    internal class BlackQueen : Piece
    {
        public BlackQueen()
        {
            Name = "[q]";
            PointValue = 9;
            BelongsTo = Player.Black;
        }
        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
