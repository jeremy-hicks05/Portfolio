using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    internal class Match
    {
        public Board Board { get; set; } = new Board();
        public bool Playing { get; set; }
    }
}
