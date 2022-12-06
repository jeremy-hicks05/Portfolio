namespace ConsoleChessV3
{
    using static ConsoleChessV3.Enums.Notation;
    internal static class ChessBoard
    {
        public static Space[][] Spaces;
        public static void InitBoard()
        {
            // build 8x8 Chess Board
            Spaces = new Space[8][];

            for(int i = 0; i < 8; i++)
            {
                Spaces[i] = new Space[8];
                for (int j = 0; j < 8; j++)
                {
                    Spaces[i][j] = new Space();
                }
            }
            Console.WriteLine("Board Initiated");
        }

        public static void GetInitialSpaceInput()
        {
            // get user input (A-H) and (1-8) for initial space
        }

        public static void GetTargetSpaceInput()
        {
            // get user input (A-H) and (1-8) for target space
        }

        public static void ChangeTurn()
        {
            // change from White to Black or Black to White when move has been performed
        }

        public static void PrintBoard()
        {
            for(int i = C["A"]; i <= C["H"]; i++)
            {
                Console.Write((i+1).ToString());
                for (int j = R["1"]; j <= R["8"]; j++)
                {
                    Console.Write(Spaces[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A  B  C  D  E  F  G  H");
        }
    }
}
