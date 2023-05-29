using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV5.Abstract
{
    internal abstract class Piece
    {
        internal bool Owner { get; set; }
        internal string Icon { get; set; }
    }
}
