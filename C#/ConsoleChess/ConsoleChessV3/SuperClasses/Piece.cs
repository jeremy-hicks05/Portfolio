namespace ConsoleChessV3.SuperClasses
{
    using ConsoleChessV3.Enums;
    using ConsoleChessV3.Interfaces;
    internal class Piece : IPiece
    {
        public string Name { get; set; }
        public bool HasMoved { get; set; }
        public int PointValue { get; set; }
        public Player BelongsTo { get; set; }
        public void BuildListOfSpacesToInspect()
        {

        }

        public virtual bool CanLegallyTryToCaptureFromSpaceToSpace()
        {
            return true;
        }

        public virtual bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return true;
        }

        public IChessMove Capture()
        {
            throw new NotImplementedException();
        }

        public bool IsBlocked()
        {
            return false;
        }

        public IChessMove Move()
        {
            throw new NotImplementedException();
        }

        public bool TryCapture()
        {
            return true;
        }

        public bool TryMove()
        {
            return true;
        }

        //--- getters and setters ---//
        public int GetPointValue()
        {
            return PointValue;
        }

        public Player GetBelongsTo()
        {
            return BelongsTo;
        }

        public bool GetHasMoved()
        {
            return HasMoved;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
