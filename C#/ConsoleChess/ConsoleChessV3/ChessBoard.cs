namespace ConsoleChessV3
{
    using ConsoleChessV3.Builders;
    using ConsoleChessV3.Enums;
    using ConsoleChessV3.Moves;
    using ConsoleChessV3.Pieces.Black;
    using ConsoleChessV3.Pieces.White;
    using ConsoleChessV3.SuperClasses;
    using System.Text.RegularExpressions;
    using static ConsoleChessV3.Enums.Notation;
    using Capture = Moves.Capture; // prevent ambiguity between Moves.Capture and RegExp.Capture

    internal class ChessBoard
    {
        public static Space[][]? Spaces;
        public static Stack<ChessMove?>? MovesPlayed = new();
        public static ChessMove? NextMove;

        public static Space? InitialSpace;
        public static Space? TargetSpace;

        private static Player Turn;

        public static Space WhiteKingSpace;
        public static Space BlackKingSpace;

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
                    Spaces[i][j].Column = i;
                    Spaces[i][j].Row = j;
                }
            }

            for (int i = C["A"]; i <= C["H"]; i++)
            {
                Spaces[i][R["7"]].Piece = new BlackPawn();
            }

            for (int i = C["A"]; i <= C["H"]; i++)
            {
                Spaces[i][R["2"]].Piece = new WhitePawn();
            }

            Spaces[C["A"]][R["8"]].Piece = new BlackRook();
            Spaces[C["B"]][R["8"]].Piece = new BlackKnight();
            Spaces[C["C"]][R["8"]].Piece = new BlackBishop();
            Spaces[C["D"]][R["8"]].Piece = new BlackQueen();
            Spaces[C["E"]][R["8"]].Piece = new BlackKing();
            BlackKingSpace = Spaces[C["E"]][R["8"]];
            Spaces[C["F"]][R["8"]].Piece = new BlackBishop();
            Spaces[C["G"]][R["8"]].Piece = new BlackKnight();
            Spaces[C["H"]][R["8"]].Piece = new BlackRook();

            Spaces[C["A"]][R["1"]].Piece = new WhiteRook();
            Spaces[C["B"]][R["1"]].Piece = new WhiteKnight();
            Spaces[C["C"]][R["1"]].Piece = new WhiteBishop();
            Spaces[C["D"]][R["1"]].Piece = new WhiteQueen();
            Spaces[C["E"]][R["1"]].Piece = new WhiteKing();
            WhiteKingSpace = Spaces[C["E"]][R["1"]];
            Spaces[C["F"]][R["1"]].Piece = new WhiteBishop();
            Spaces[C["G"]][R["1"]].Piece = new WhiteKnight();
            Spaces[C["H"]][R["1"]].Piece = new WhiteRook();

            Turn = Player.White;

            FindAllSpacesAttacked();

            //Console.WriteLine("Board Initiated");
        }

        public static void GetInitialSpaceInput()
        {
            // get user input (A-H) and (1-8) for initial space
            string selectedPieceColumn = "Z";
            string selectedPieceRow = "0";
            while (!(Regex.Match(selectedPieceColumn!, "^[A-Ha-h]$").Success))
            {
                Console.WriteLine("Please enter a letter (A-H) or T to TakeBack");
                selectedPieceColumn = Console.ReadLine()!.ToUpper();

                if (selectedPieceColumn == "T")
                {
                    TakeBackMove();
                }
            }

            while (!(Regex.Match(selectedPieceRow!, "^[1-8]$").Success))
            {
                Console.WriteLine("Please enter a number (1-8)");
                selectedPieceRow = Console.ReadLine()!.ToUpper();
            }

            SetInitialSpaceFromInput(selectedPieceColumn, selectedPieceRow);
        }

        public static void GetTargetSpaceInput()
        {
            // get user input (A-H) and (1-8) for initial space
            string selectedPieceColumn = "Z";
            string selectedPieceRow = "0";
            while (!(Regex.Match(selectedPieceColumn!, "^[A-Ha-h]$").Success))
            {
                Console.WriteLine("Please enter a letter (A-H)");
                selectedPieceColumn = Console.ReadLine()!.ToUpper();
            }

            while (!(Regex.Match(selectedPieceRow!, "^[1-8]$").Success))
            {
                Console.WriteLine("Please enter a number (1-8)");
                selectedPieceRow = Console.ReadLine()!.ToUpper();
            }

            SetTargetSpaceFromInput(selectedPieceColumn, selectedPieceRow);
        }

        public static void SetInitialSpaceFromInput(string column, string row)
        {
            if (Spaces is not null)
            {
                InitialSpace = Spaces[C[column]][R[row]];
            }
        }

        public static void SetTargetSpaceFromInput(string column, string row)
        {
            if (Spaces is not null)
            {
                TargetSpace = Spaces[C[column]][R[row]];
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
            if (Turn == Player.White)
            {
                Turn = Player.Black;
            }
            else
            {
                Turn = Player.White;
            }
        }

        public static void PrintBoard()
        {
            Console.Clear();
            if (Spaces is not null)
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
        }

        public static void PlayMove()
        {
            if (InitialSpace is not null &&
                TargetSpace is not null &&
                InitialSpace.Piece is not null)
            {
                if (InitialSpace.Piece.GetBelongsTo() == Turn)
                {
                    NextMove = MoveBuilder.Build(InitialSpace, TargetSpace);

                    if (NextMove is not null)
                    {
                        Console.WriteLine(NextMove.StartingPiece);
                        Console.WriteLine(NextMove.TargetPiece);

                        if (NextMove.IsValidChessMove())
                        {
                            NextMove.Perform();
                            SaveMoveInHistory();
                            ChangeTurn();
                            PrintBoard();
                        }
                    }
                }
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
            if (MovesPlayed is not null && MovesPlayed.Count > 0)
            {
                ChessMove? lastMove = MovesPlayed.Pop();
                if (lastMove is not null)
                {
                    lastMove.Reverse();
                    ChangeTurn();
                }
            }
            PrintBoard();
        }

        public static void ShowMoveHistory()
        {
            if (MovesPlayed is not null)
            {
                foreach (ChessMove? m in MovesPlayed)
                {
                    if (m is not null)
                    {
                        Console.WriteLine("Move Type: " + m.GetType());
                        Console.WriteLine("Starting Space: " + m.StartingSpace);
                        Console.WriteLine("Ending Space: " + m.TargetSpace);
                        Console.WriteLine("Restore Space: " + m.RestoreSpace);

                        Console.WriteLine("Starting Piece: " + m.StartingPiece);
                        Console.WriteLine("Ending Piece: " + m.TargetPiece);
                        Console.WriteLine("Restore Piece: " + m.RestorePiece);
                    }
                }
            }
            else
            {
                Console.WriteLine("No moves have been played");
            }
        }

        public static void FindAllSpacesAttacked()
        {
            if (Spaces is not null)
            {
                // reset UnderAttack flags
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        // reset being attacked flags
                        Spaces[i][j].IsUnderAttackByBlack = false;
                        Spaces[i][j].IsUnderAttackByWhite = false;
                    }
                }

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Spaces[i][j].Piece is not null &&
                            Spaces[i][j].Piece.GetBelongsTo() == Player.White)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int m = 0; m < 8; m++)
                                {
                                    if (Spaces[i][j].Piece is not null &&
                                        (Spaces![i][j].Piece
                                        .CanLegallyTryToMoveFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece
                                        .IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        Spaces[k][m].IsUnderAttackByWhite = true;
                                    }
                                    if (Spaces[i][j].Piece is not null &&
                                        (Spaces![i][j].Piece
                                        .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece
                                        .IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        Spaces[k][m].IsUnderAttackByWhite = true;
                                    }
                                }
                            }
                        }
                        if (Spaces[i][j].Piece is not null &&
                            Spaces[i][j].Piece.GetBelongsTo() == Player.Black)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int m = 0; m < 8; m++)
                                {
                                    if (Spaces[i][j].Piece is not null &&
                                        (Spaces![i][j].Piece
                                        .CanLegallyTryToMoveFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece
                                        .IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        Spaces[k][m].IsUnderAttackByBlack = true;
                                    }
                                    if (Spaces[i][j].Piece is not null &&
                                        (Spaces[i][j].Piece
                                        .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece
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
        }

        public static bool KingIsInCheck()
        {
            FindAllSpacesAttacked();
            if (Turn == Player.White && WhiteKingSpace!.IsUnderAttackByBlack)
            {
                return true;
            }
            else if (Turn == Player.Black && BlackKingSpace!.IsUnderAttackByWhite)
            {
                return true;
            }
            return false;
        }

        public static bool WhiteIsCheckMated()
        {
            FindAllSpacesAttacked();
            if (WhiteKingSpace!.IsUnderAttackByBlack)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Spaces is not null &&
                            Spaces[i][j].Piece is not null &&
                                Spaces![i][j].Piece.GetBelongsTo() == Player.White)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int m = 0; m < 8; m++)
                                {
                                    if ((Spaces![i][j].Piece
                                        .CanLegallyTryToMoveFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        if (Spaces[i][j].Piece.TryMove(Spaces[i][j], Spaces[k][m]))
                                        {
                                            //Console.WriteLine("White is not checkmated!");
                                            return false;
                                        }
                                    }
                                    if ((Spaces![i][j].Piece
                                        .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        if (Spaces[i][j].Piece.TryCapture(Spaces[i][j], Spaces[k][m]))
                                        {
                                            //Console.WriteLine("White is not checkmated!");
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
            //Console.WriteLine("White is not in check!");
            return false;
        }
        public static bool WhiteIsStaleMated()
        {
            FindAllSpacesAttacked();
            if (!(WhiteKingSpace!.IsUnderAttackByBlack))
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Spaces![i][j].Piece?.GetBelongsTo() == Player.White)
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
                                            //Console.WriteLine("White is not stalemated!");
                                            return false;
                                        }
                                    }
                                    if ((Spaces![i][j].Piece!
                                        .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece!.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        if (Spaces[i][j].Piece!.TryCapture(Spaces[i][j], Spaces[k][m]))
                                        {
                                            //Console.WriteLine("White is not stalemated!");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("WHITE IS STALEMATED!");
                return true;
            }
            //Console.WriteLine("White is in check!");
            return false;
        }
        public static bool BlackIsCheckMated()
        {
            FindAllSpacesAttacked();
            if (BlackKingSpace!.IsUnderAttackByWhite)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {

                        if (Spaces![i][j].Piece is not null &&
                            Spaces![i][j].Piece.GetBelongsTo() == Player.Black)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int m = 0; m < 8; m++)
                                {
                                    if (Spaces![i][j].Piece
                                        .CanLegallyTryToMoveFromSpaceToSpace(Spaces[i][j], Spaces[k][m]) &&
                                        !(Spaces![i][j].Piece.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        if (Spaces[i][j].Piece.TryMove(Spaces[i][j], Spaces[k][m]))
                                        {
                                            //Console.WriteLine("Black is not checkmated!");
                                            return false;
                                        }
                                    }
                                    if ((Spaces![i][j].Piece
                                        .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        if (Spaces[i][j].Piece.TryCapture(Spaces[i][j], Spaces[k][m]))
                                        {
                                            //Console.WriteLine("Black is not checkmated!");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("BLACK IS CHECKMATED!");
                return true;
            }
            //Console.WriteLine("Black is not in check!");
            return false;
        }
        public static bool BlackIsStaleMated()
        {
            FindAllSpacesAttacked();
            if (!(BlackKingSpace!.IsUnderAttackByWhite))
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Spaces![i][j].Piece?.GetBelongsTo() == Player.Black)
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
                                            //Console.WriteLine("Black is not stalemated!");
                                            return false;
                                        }
                                    }
                                    if ((Spaces![i][j].Piece!
                                        .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece!.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        if (Spaces[i][j].Piece!.TryCapture(Spaces[i][j], Spaces[k][m]))
                                        {
                                            //Console.WriteLine("Black is not stalemated!");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("BLACK IS STALEMATED!");
                return true;
            }
            //Console.WriteLine("Black is in check!");
            return false;
        }
    }
}
