namespace ConsoleChessV3
{
    using ConsoleChessV3.Interfaces;
    using ConsoleChessV3.Moves;
    using ConsoleChessV3.Pieces.Black;
    using ConsoleChessV3.Pieces.White;
    using ConsoleChessV3.SuperClasses;
    using static ConsoleChessV3.Enums.Notation;
    internal class ChessBoard
    {
        public static Space[][]? Spaces;
        public static Stack<ChessMove?>? MovesPlayed = new();
        public static ChessMove? NextMove;

        public static Space? InitialSpace;
        public static Space? TargetSpace;
        public static Space? AffectedSpace;

        public static void InitBoard()
        {
            // build 8x8 Chess Board
            Spaces = new Space[8][];

            for (int i = C["A"]; i <= C["H"]; i++)
            {
                Spaces[i] = new Space[8];
                for (int j = R["1"]; j <= R["8"]; j++)
                {
                    Spaces[i][j] = new Space();
                }
            }

            for(int i = C["A"]; i <= C["H"]; i++)
            {
                Spaces[i][R["7"]].Piece = new BlackPawn();
            }

            for (int i = C["A"]; i <= C["H"]; i++)
            {
                Spaces[i][R["2"]].Piece = new WhitePawn();
            }

            Spaces[C["A"]][R["8"]].Piece = new BlackRook();
            Spaces[C["B"]][R["8"]].Piece = new BlackBishop();
            Spaces[C["C"]][R["8"]].Piece = new BlackKnight();
            Spaces[C["D"]][R["8"]].Piece = new BlackQueen();
            Spaces[C["E"]][R["8"]].Piece = new BlackKing();
            Spaces[C["F"]][R["8"]].Piece = new BlackBishop();
            Spaces[C["G"]][R["8"]].Piece = new BlackKnight();
            Spaces[C["H"]][R["8"]].Piece = new BlackRook();

            Spaces[C["A"]][R["1"]].Piece = new WhiteRook();
            Spaces[C["B"]][R["1"]].Piece = new WhiteBishop();
            Spaces[C["C"]][R["1"]].Piece = new WhiteKnight();
            Spaces[C["D"]][R["1"]].Piece = new WhiteQueen();
            Spaces[C["E"]][R["1"]].Piece = new WhiteKing();
            Spaces[C["F"]][R["1"]].Piece = new WhiteBishop();
            Spaces[C["G"]][R["1"]].Piece = new WhiteKnight();
            Spaces[C["H"]][R["1"]].Piece = new WhiteRook();

            //Console.WriteLine("Board Initiated");
        }

        public static void GetInitialSpaceInput()
        {
            // TODO: Clean input
            // get user input (A-H) and (1-8) for initial space
            Console.WriteLine("Please enter Letter for Initial Space");
            int column = int.Parse(Console.ReadLine());

            // TODO: Clean input
            Console.WriteLine("Please enter Number for Initial Space");
            int row = int.Parse(Console.ReadLine());

            SetInitialSpaceFromInput(column, row);
        }

        public static void GetTargetSpaceInput()
        {
            // TODO: Clean input
            // get user input (A-H) and (1-8) for target space
            Console.WriteLine("Please enter Letter for Target Space");
            int column = int.Parse(Console.ReadLine());

            // TODO: Clean input
            Console.WriteLine("Please enter Number for Target Space");
            int row = int.Parse(Console.ReadLine());

            SetTargetSpaceFromInput(column, row);
        }

        public static void SetInitialSpaceFromInput(int column, int row)
        {
            // get user input (A-H) and (1-8) for initial space
            if (Spaces is not null)
            {
                InitialSpace = Spaces[column][row];
            }
        }

        public static void SetTargetSpaceFromInput(int column, int row)
        {
            // get user input (A-H) and (1-8) for initial space
            if (Spaces is not null)
            {
                TargetSpace = Spaces[column][row];
            }
        }

        public static Space? GetInitialSpace()
        {
            return InitialSpace;
        }

        public static Space? GetTargetSpace()
        {
            return TargetSpace;
        }


        public static void ChangeTurn()
        {
            // change from White to Black or Black to White when move has been performed
        }

        public static void PrintBoard()
        {
            for (int j = R["8"]; j >= R["1"]; j--)
            {
                Console.Write((j + 1).ToString());
                for (int i = C["A"]; i <= C["H"]; i++)
                {
                    Console.Write(Spaces[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A  B  C  D  E  F  G  H");
        }

        public static void PlayMove()
        {
            if (InitialSpace is not null && TargetSpace is not null)
            {
                NextMove = new Move(InitialSpace, TargetSpace, TargetSpace);
            }
        }

        public static void SaveMoveInHistory()
        {
            if (MovesPlayed is not null)
            {
                MovesPlayed.Push(NextMove);
            }
        }

        public static void TakeBackMove()
        {
            if (MovesPlayed is not null)
            {
                ChessMove? lastMove = MovesPlayed.Pop();
                if (lastMove is not null)
                {
                    lastMove.TargetSpace.Clear();
                    lastMove.AffectedSpace.Piece = lastMove.AffectedPiece;
                    lastMove.StartingSpace.Piece = lastMove.StartingPiece;
                }
            }
        }
    }
}
