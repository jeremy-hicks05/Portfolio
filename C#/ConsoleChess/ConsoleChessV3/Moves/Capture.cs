namespace ConsoleChessV3.Moves
{
    using ConsoleChessV3.Interfaces;
    using ConsoleChessV3.SuperClasses;

    internal class Capture : ChessMove
    {
        public Capture(Space startingSpace, Space endingSpace) : base(startingSpace, endingSpace)
        {

        }
        public override void Perform()
        {
            StartingPiece.Capture(StartingSpace, TargetSpace);
        }
    }
}
