using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.Utility
{
    internal static class UserInput
    {
        public static int GetColumnInput()
        {
            int column;
            Console.WriteLine("Please enter column (a-h)");
            string columnString = Console.ReadLine();

            column = 
                LetterToCoordinate.C(columnString);
            return column;
        }

        public static int GetRowInput()
        {
            int row;
            Console.WriteLine("Please enter row (1-8)");
            string rowString = Console.ReadLine();

            row = LetterToCoordinate.R(rowString);
            return row;
        }
    }
}
