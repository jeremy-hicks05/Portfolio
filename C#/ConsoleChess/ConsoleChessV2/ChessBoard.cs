

namespace ConsoleChessV2
{
    using ConsoleChessV2.Pieces;
    using System.Text.RegularExpressions;
    using static Notation;
    internal static class ChessBoard
    {
        public static Space[][]? Spaces { get; set; }
        public static Space? WhiteKingSpace { get; set; }
        public static Space? BlackKingSpace { get; set; }
        public static Player turn;

        public static void InitBoard()
        {

            Console.WriteLine("Initializing Board...");

            Spaces = new Space[8][];

            if (Spaces is not null)
            {
                for (int i = C["A"]; i <= C["H"]; i++)
                {
                    Spaces[i] = new Space[8];
                    for (int j = R["1"]; j <= R["8"]; j++)
                    {
                        // populate empty spaces
                        Spaces[i][j] = new Space(i, j, new Piece());
                    }
                }

                // populate white back line
                Spaces[C["A"]][R["1"]].Piece = new WhiteRook();
                Spaces[C["B"]][R["1"]].Piece = new WhiteKnight();
                Spaces[C["C"]][R["1"]].Piece = new WhiteBishop();
                Spaces[C["D"]][R["1"]].Piece = new WhiteQueen();
                Spaces[C["E"]][R["1"]].Piece = new WhiteKing();
                Spaces[C["F"]][R["1"]].Piece = new WhiteBishop();
                Spaces[C["G"]][R["1"]].Piece = new WhiteKnight();
                Spaces[C["H"]][R["1"]].Piece = new WhiteRook();

                // populate white pawns
                Spaces[C["A"]][R["2"]].Piece = new WhitePawn();
                Spaces[C["B"]][R["2"]].Piece = new WhitePawn();
                Spaces[C["C"]][R["2"]].Piece = new WhitePawn();
                Spaces[C["D"]][R["2"]].Piece = new WhitePawn();
                Spaces[C["E"]][R["2"]].Piece = new WhitePawn();
                Spaces[C["F"]][R["2"]].Piece = new WhitePawn();
                Spaces[C["G"]][R["2"]].Piece = new WhitePawn();
                Spaces[C["H"]][R["2"]].Piece = new WhitePawn();

                // populate black pawns
                Spaces[C["A"]][R["7"]].Piece = new BlackPawn();
                Spaces[C["B"]][R["7"]].Piece = new BlackPawn();
                Spaces[C["C"]][R["7"]].Piece = new BlackPawn();
                Spaces[C["D"]][R["7"]].Piece = new BlackPawn();
                Spaces[C["H"]][R["7"]].Piece = new BlackPawn();
                Spaces[C["E"]][R["7"]].Piece = new BlackPawn();
                Spaces[C["F"]][R["7"]].Piece = new BlackPawn();
                Spaces[C["G"]][R["7"]].Piece = new BlackPawn();

                // populate black back line
                Spaces[C["A"]][R["8"]].Piece = new BlackRook();
                Spaces[C["B"]][R["8"]].Piece = new BlackKnight();
                Spaces[C["C"]][R["8"]].Piece = new BlackBishop();
                Spaces[C["D"]][R["8"]].Piece = new BlackQueen();
                Spaces[C["E"]][R["8"]].Piece = new BlackKing();
                Spaces[C["F"]][R["8"]].Piece = new BlackBishop();
                Spaces[C["G"]][R["8"]].Piece = new BlackKnight();
                Spaces[C["H"]][R["8"]].Piece = new BlackRook();

                WhiteKingSpace = Spaces[C["E"]][R["1"]];
                BlackKingSpace = Spaces[C["E"]][R["8"]];

                turn = Player.White;
            }
        }

        public static void PrintBoard()
        {
            Console.Clear();
            if (Spaces is not null)
            {
                for (int j = R["8"]; j >= R["1"]; j--)
                {
                    Console.Write(j + 1);
                    for (int i = C["A"]; i <= C["H"]; i++)
                    {
                        Console.Write(Spaces[i][j].Piece?.Name);
                        if (i == C["H"])
                        {
                            Console.WriteLine();
                        }
                        if (i == C["H"] && j == R["1"])
                        {
                            Console.WriteLine("  A  B  C  D  E  F  G  H");
                        }
                    }
                }
            }
        }

        public static Space UserSelectsSpace()
        {
            string? selectedPieceColumn = "Z";
            string? selectedPieceRow = "0";
            while (!(Regex.Match(selectedPieceColumn!, "[A-Ha-h]").Success))
            {
                Console.WriteLine("Please enter a letter (A-H)");
                selectedPieceColumn = Console.ReadLine();
            }

            while (!(Regex.Match(selectedPieceRow!, "[1-8]").Success))
            {
                Console.WriteLine("Please enter a number (1-8)");
                selectedPieceRow = Console.ReadLine();
            }

            //Console.WriteLine("Your piece is a " +
            //Spaces?[C[selectedPieceColumn?.ToUpper()!]]
            //       [R[selectedPieceRow!]].Piece?.Name);

            return Spaces![C[selectedPieceColumn?.ToUpper()!]]
                          [R[selectedPieceRow!]];
        }

        public static void ChangeTurn()
        {
            if (turn == Player.White)
            {
                turn = Player.Black;
            }
            else
            {
                turn = Player.White;
            }
        }

        public static void FindAllSpacesAttacked()
        {
            // reset UnderAttack flags
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // reset being attacked flags
                    Spaces![i][j].IsUnderAttackByBlack = false;
                    Spaces![i][j].IsUnderAttackByWhite = false;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Spaces![i][j].Piece?.BelongsTo == Player.White)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            for (int m = 0; m < 8; m++)
                            {
                                if ((Spaces![i][j].Piece!
                                    .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                    !(Spaces![i][j].Piece!
                                    .IsBlocked(Spaces[i][j], Spaces[k][m])))
                                {
                                    Spaces[k][m].IsUnderAttackByWhite = true;
                                }
                            }
                        }
                    }
                    if (Spaces[i][j].Piece!.BelongsTo == Player.Black)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            for (int m = 0; m < 8; m++)
                            {
                                if ((Spaces[i][j].Piece!
                                    .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                    !(Spaces![i][j].Piece!
                                    .IsBlocked(Spaces[i][j], Spaces[k][m])))
                                {
                                    Spaces[k][m].IsUnderAttackByBlack = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        // if current turn's king is in check
        // if piece can move anywhere that results in the current turn's king not being in check, it is not checkmate

        // if current turn's king is not in check
        // if no piece can move anywhere without putting its own king in check
        public static bool WhiteIsCheckMated()
        {
            if (WhiteKingSpace!.IsUnderAttackByBlack)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Spaces![i][j].Piece?.BelongsTo == Player.White)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int m = 0; m < 8; m++)
                                {
                                    if ((Spaces![i][j].Piece!
                                        .CanLegallyTryToMoveFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece!.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        if (Spaces[i][j].Piece!.TryMove(Spaces[i][j], Spaces[k][m]))
                                        {
                                            Console.WriteLine("White is not checkmated!");
                                            return false;
                                        }
                                    }
                                    if ((Spaces![i][j].Piece!
                                        .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece!.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        if (Spaces[i][j].Piece!.TryCapture(Spaces[i][j], Spaces[k][m]))
                                        {
                                            Console.WriteLine("White is not checkmated!");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("WHITE IS CHECKMATED!");
                return true;
            }
            Console.WriteLine("White is not in check!");
            return false;
        }
    }
}