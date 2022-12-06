using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV3.Extensions
{
    internal static class MyExtensions
    {
        public static bool DistanceTo(this Space fromSpace, Space toSpace)
        {
            return false;
            //return Math.Abs(fromSpace.Column - toSpace.Column);
        }
    }
}
