﻿namespace ConsoleChessV3.Enums
{
    /// <summary>
    /// Allows for the use of C["A"] to represent column A in the 2D Spaces array
    /// for clarity and ease of coding
    /// </summary>
    internal class Notation
    {
        public static Dictionary<string, int> C = new()
        {
            {"A", 0},
            {"B", 1},
            {"C", 2},
            {"D", 3},
            {"E", 4},
            {"F", 5},
            {"G", 6},
            {"H", 7},

            {"1", 0},
            {"2", 1},
            {"3", 2},
            {"4", 3},
            {"5", 4},
            {"6", 5},
            {"7", 6},
            {"8", 7}
        };

        public static Dictionary<string, int> R = new()
        {
            {"1", 0},
            {"2", 1},
            {"3", 2},
            {"4", 3},
            {"5", 4},
            {"6", 5},
            {"7", 6},
            {"8", 7}
        };
    }
}
