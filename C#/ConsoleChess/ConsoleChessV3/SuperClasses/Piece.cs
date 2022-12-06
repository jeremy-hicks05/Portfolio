namespace ConsoleChessV3.SuperClasses
{
    using ConsoleChessV3.Enums;
    using ConsoleChessV3.Interfaces;
    internal class Piece : IPiece
    {
        public string Name { get; set; } = $" ";
        public bool HasMoved { get; set; }
        public int PointValue { get; set; }
        public Player BelongsTo { get; set; }

        public virtual void BuildListOfSpacesToInspect()
        {

        }

        public virtual bool CanLegallyTryToCaptureFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }

        public virtual bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }

        public virtual bool IsBlocked()
        {
            return true;
        }

        public virtual bool TryCapture()
        {
            return false;
        }

        public virtual void Capture(Space fromSpace, Space toSpace)
        {
            toSpace.Piece = fromSpace.Piece;
            fromSpace.Clear();
        }

        public virtual bool TryMove()
        {
            return false;
        }

        public virtual void Move(Space fromSpace, Space toSpace)
        {
            toSpace.Piece = fromSpace.Piece;
            fromSpace.Clear();
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
