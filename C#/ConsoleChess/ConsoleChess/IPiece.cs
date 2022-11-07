using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    internal interface IPiece
    {
        public bool Move(Space from, Space to);
    }
}
