namespace ConsoleChessV3.Moves
{
    using ConsoleChessV3.SuperClasses;

    internal class Capture : ChessMove
    {
        public Capture(Space startingSpace, Space endingSpace) : base(startingSpace, endingSpace)
        {

        }
        public override void Perform()
        {
            if (StartingSpace is not null && StartingSpace.Piece is not null)
            {
                if (StartingSpace.Piece.CanLegallyTryToCaptureFromSpaceToSpace(StartingSpace, TargetSpace))
                {
                    StartingSpace.Piece.Capture(StartingSpace, TargetSpace);
                }
            }
        }


        public override bool IsValidChessMove()
        {
            if (StartingSpace is not null && StartingSpace.Piece is not null)
            {
                if (StartingSpace.Piece.CanLegallyTryToCaptureFromSpaceToSpace(StartingSpace, TargetSpace))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
