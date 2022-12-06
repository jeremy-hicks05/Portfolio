namespace ConsoleChessV3.Moves
{
    using ConsoleChessV3.Interfaces;
    using ConsoleChessV3.SuperClasses;

    internal class Castle : ChessMove
    {
        public Castle(Space startingSpace, Space endingSpace, Space affectedSpace) : base(startingSpace, endingSpace, affectedSpace)
        {
        }
    }
}
