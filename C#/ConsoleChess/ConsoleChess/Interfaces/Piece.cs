using ConsoleChess.Enums;

namespace ConsoleChess.Interfaces
{
    internal class Piece : IPiece
    {
        public string Name { get; set; }
        public Player belongsToPlayer = Player.None;

        //public Piece(string name, Player belongsTo)
        //{
        //    Name = name;
        //    this.belongsToPlayer = belongsTo;
        //}

        public Piece(string name, Player belongsTo)
        {
            Name = name;
            belongsToPlayer = belongsTo;
        }

        public virtual void MoveTo(Space spaceMovedTo)
        {
            spaceMovedTo.Piece.Name = Name;
            Name = "[ ]";
        }

        public virtual Space[] GetSpacesAvaiableToMoveTo()
        {
            // return array of spaces particular piece can move to
            return null;
        }

        public virtual bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            return false;
        }
    }
}
