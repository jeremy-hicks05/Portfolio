using ConsoleChess.Enums;

namespace ConsoleChess.Interfaces
{
    internal class Piece : IPiece
    {
        public string Name { get; set; }
        public Player belongsToPlayer = Player.None;
        public bool hasMoved;

        public Piece(string name, Player belongsTo)
        {
            Name = name;
            belongsToPlayer = belongsTo;
            hasMoved = false;
        }

        public virtual Space[]? GetSpacesAvaiableToMoveTo()
        {
            // return array of spaces particular piece can move to
            return null;
        }

        public virtual bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }

        public virtual bool CanAttackSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
