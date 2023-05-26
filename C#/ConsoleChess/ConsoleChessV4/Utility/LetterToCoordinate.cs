using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV4.Utility
{
    internal static class LetterToCoordinate
    {
        public static int R(string row)
        {
            int translatedRow = -1;

            switch (row.ToUpper())
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8": translatedRow = int.Parse(row.ToString()) - 1;
                    break;

            }

            return translatedRow;
        }

        public static int C(string column)
        {
            int translatedRow = -1;

            switch (column.ToUpper())
            {
                case "A": translatedRow = 0; break;
                case "B": translatedRow = 1; break;
                case "C": translatedRow = 2; break;
                case "D": translatedRow = 3; break;
                case "E": translatedRow = 4; break;
                case "F": translatedRow = 5; break;
                case "G": translatedRow = 6; break;
                case "H": translatedRow = 7; break;
            }

            return translatedRow;
        }
    }
}
