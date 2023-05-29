using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV5.Utility
{
    internal static class Conversion
    {
        public static int ConvertColumnToInt(string column) 
        { 
            switch (column.ToUpper())
            {
                case "A": return 0;
                case "B": return 1;
                case "C": return 2;
                case "D": return 3;
                case "E": return 4;
                case "F": return 5;
                case "G": return 6;
                case "H": return 7;
                default: return -1;
            }
        }

        public static int ConvertRowToInt(string row) 
        {
            switch (row.ToUpper())
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                    return int.Parse(row) - 1;
                default: return -1;
            }
        }
    }
}
