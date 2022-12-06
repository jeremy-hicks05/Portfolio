namespace ConsoleChessV3.Moves
{
    using ConsoleChessV3.Interfaces;
    using ConsoleChessV3.SuperClasses;

    internal class EnPassant : ChessMove
    {
        public EnPassant(Space startingSpace, Space endingSpace) : base(startingSpace, endingSpace)
        {
            StartingSpace = startingSpace;
        }
    }
}
