using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.Interfaces
{
    internal class Piece : IPiece
    {
        public string Name { get; set; }

        public Piece(string name)
        {
            Name = name;
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

        //public void Clear()
        //{
        //    this.Name = "[ ]";
        //}
    }
}
