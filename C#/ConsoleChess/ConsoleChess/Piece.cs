using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    internal class Piece : IPiece
    {
        public string Name { get; set; }

        public Piece(string name)
        {
            Name = name;
        }

        public bool Move(Space from, Space to)
        {
            return true;
        }
    }
}
