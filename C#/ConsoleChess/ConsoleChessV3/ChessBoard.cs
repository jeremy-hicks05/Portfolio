using ConsoleChessV3.ChessMoves.Subclasses;

namespace ConsoleChessV3
{
    using ConsoleChessV3.Builders;
    using ConsoleChessV3.Enums;
    using ConsoleChessV3.ChessMoves;
    using ConsoleChessV3.Pieces.Black;
    using ConsoleChessV3.Pieces.Subclasses;
    using ConsoleChessV3.Pieces.White;
    using System.Text.RegularExpressions;
    using static ConsoleChessV3.Enums.Notation;
    using Capture = Capture; // prevent ambiguity between Moves.Capture and RegExp.Capture

    internal class ChessBoard
    {
        public static Space[][]? Spaces;
        public static Stack<ChessMove?>? MovesPlayed = new();
        public static ChessMove? NextMove;

        public static Space? InitialSpace;
        public static Space? TargetSpace;

        public static Player Turn;

        public static Space? WhiteKingSpace;
        public static Space? BlackKingSpace;

        /// <summary>
        /// Populates ChessBoard with all the pieces, sets first turn as White
        /// </summary>
        public static void InitBoard()
        {
            // build 8x8 Chess Board
            Spaces = new Space[8][];

            for (int i = C["A"]; i <= C["H"]; i++)
            {
                Spaces[i] = new Space[8];
                for (int j = R["1"]; j <= R["8"]; j++)
                {
                    Spaces[i][j] = new();
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

        /// <summary>
        /// Verifies and gets the Letter (Column) and Row (Number) of initial space
        /// </summary>
        public static void GetInitialSpaceInput()
        {
            // get user input (A-H) and (1-8) for initial space
            string selectedPieceColumn = "Z";
            string selectedPieceRow = "0";
            while (!(Regex.Match(selectedPieceColumn!, "^[A-Ha-h]$").Success))
            {

                if (MovesPlayed is not null && MovesPlayed.Count > 0)
                {
                    Console.WriteLine("Please enter a letter (A-H) or T to TakeBack or M to list move history");
                }
                else
                {
                    Console.WriteLine("Please enter a letter (A-H)");
                }
                selectedPieceColumn = Console.ReadLine()!.ToUpper();

                if (selectedPieceColumn == "T")
                {
                    TakeBackMove();
                }
                else if (selectedPieceColumn == "M")
                {
                    ShowMoveHistory();
                }
                PrintBoard();
            }

            while (!(Regex.Match(selectedPieceRow!, "^[1-8]$").Success))
            {
                Console.WriteLine(selectedPieceColumn.ToLower());
                Console.WriteLine("Please enter a number (1-8)");
                selectedPieceRow = Console.ReadLine()!.ToUpper();
                PrintBoard();
            }

            SetInitialSpaceFromInput(selectedPieceColumn, selectedPieceRow);
        }

        /// <summary>
        /// Verifies and gets the Letter (Column) and Row (Number) of target space
        /// </summary>
        public static void GetTargetSpaceInput()
        {
            // get user input (A-H) and (1-8) for initial space
            string selectedPieceColumn = "Z";
            string selectedPieceRow = "0";
            while (!(Regex.Match(selectedPieceColumn!, "^[A-Ha-h]$").Success))
            {
                if (InitialSpace is not null)
                {
                    Console.WriteLine(InitialSpace.PrintNotation() + "->");
                }
                Console.WriteLine("Please enter a letter (A-H)");
                selectedPieceColumn = Console.ReadLine()!.ToUpper();
            }

            while (!(Regex.Match(selectedPieceRow!, "^[1-8]$").Success))
            {
                PrintBoard();
                if (InitialSpace is not null)
                {
                    Console.Write(InitialSpace.PrintNotation() + "-> ");
                }
                Console.WriteLine(selectedPieceColumn.ToLower());
                Console.WriteLine("Please enter a number (1-8)");
                selectedPieceRow = Console.ReadLine()!.ToUpper();
            }

            SetTargetSpaceFromInput(selectedPieceColumn, selectedPieceRow);
        }

        /// <summary>
        /// Sets InitialSpace property
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public static void SetInitialSpaceFromInput(string column, string row)
        {
            if (Spaces is not null)
            {
                InitialSpace = Spaces[C[column]][R[row]];
            }
        }

        /// <summary>
        /// Sets TargetSpace property
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public static void SetTargetSpaceFromInput(string column, string row)
        {
            if (Spaces is not null)
            {
                TargetSpace = Spaces[C[column]][R[row]];
            }
        }

        /// <summary>
        /// Changes turn from Black to White or White to Black
        /// </summary>
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

        /// <summary>
        /// Sets "HasJustMovedTwo" flag to false for your pawns when your turn starts
        /// </summary>
        public static void UpdateHasJustMovedTwo()
        {
            if (Spaces is not null)
            {
                // change all 'has just moved two's for player whose turn it is about to become - how?
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Spaces[i][j].Piece is not null && Spaces[i][j].Piece!.GetBelongsTo() == Turn)
                        {
                            if (Spaces[i][j].Piece is Pawn)
                            {
                                Pawn? tempPawn = Spaces[i][j].Piece as Pawn;
                                tempPawn!.HasJustMovedTwo = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Prints ChessBoard and Pieces in an 8x8 grid to the console
        /// </summary>
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
                Console.WriteLine($"-{Turn}'s turn-");
            }
        }

        /// <summary>
        /// Checks for null Space inputs, Builds NextMove, validates move, performs move
        /// Changes turn, Updates "HasJustMovedTwo", and finally Prints the Board
        /// </summary>
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
                        if (NextMove.IsValidChessMove())
                        {
                            NextMove.Perform();
                            SaveMoveToHistory();
                            ChangeTurn();
                            UpdateHasJustMovedTwo();
                            PrintBoard();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Pushes NextMove property to MoveHistory stack
        /// </summary>
        public static void SaveMoveToHistory()
        {
            if (MovesPlayed is not null)
            {
                MovesPlayed.Push(NextMove);
            }
        }

        /// <summary>
        /// Reverses the move on the top of the MoveHistory stack
        /// </summary>
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

        /// <summary>
        /// Prints out the Initial and Target spaces of the move history
        /// </summary>
        public static void ShowMoveHistory()
        {
            if (MovesPlayed is not null)
            {
                int i = 1;
                foreach (ChessMove? m in MovesPlayed.Reverse())
                {
                    if (m is not null)
                    {
                        if (m.StartingPiece.GetBelongsTo() == Player.White)
                        {
                            Console.Write(i + ":");
                            i++;
                        }

                        if (m is Capture || m is EnPassant)
                        {
                            if (m.StartingPiece is Pawn)
                            {
                                Console.Write(" " + 
                                    m.StartingSpace.PrintNotation() + "x" +
                                    m.TargetSpace.PrintNotation());
                            }
                            else
                            {
                                Console.Write(" " +
                                    m.StartingPiece.GetName() +
                                    m.StartingSpace.PrintNotation() + "x" +
                                    m.TargetPiece?.GetName() +
                                    m.TargetSpace.PrintNotation());
                            }
                        }
                        else if (m is Move)
                        {
                            if (m.StartingPiece is Pawn)
                            {
                                Console.Write(" " + m.TargetSpace.PrintNotation());
                            }
                            else
                            {
                                Console.Write(" " + m.StartingPiece.GetName());
                                Console.Write(m.TargetSpace.PrintNotation());
                            }
                        }
                        else if (m is Castle)
                        {
                            // if king side castle
                            if (TargetSpace?.Column == C["G"])
                            {
                                Console.Write(" o-o");
                            }
                            else // if queen side castle
                            {
                                Console.Write(" o-o-o");
                            }
                        }
                        else
                        {
                            Console.Write(" " +
                                m.StartingPiece.GetName() +
                                m.StartingSpace.PrintNotation() + " -> " +
                                m.TargetPiece?.GetName() +
                                m.TargetSpace.PrintNotation());
                            //Console.WriteLine("Move Type: " + m.GetType().ToString().Split(".").Last());
                        }
                        if(m.StartingPiece.GetBelongsTo() == Player.Black)
                        {
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No moves have been played");
                    }
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Iterates through the board, attempting to move every piece to every space,
        /// setting the IsUnderAttackBy{Player} property based on the owner of the piece
        /// </summary>
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
                            Spaces[i][j].Piece!.GetBelongsTo() == Player.White)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int m = 0; m < 8; m++)
                                {
                                    if (Spaces[i][j].Piece is not null &&
                                        (Spaces![i][j].Piece!
                                        .CanLegallyTryToMoveFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece!
                                        .IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        Spaces[k][m].IsUnderAttackByWhite = true;
                                    }
                                    if (Spaces[i][j].Piece is not null &&
                                        (Spaces![i][j].Piece!
                                        .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece!
                                        .IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        Spaces[k][m].IsUnderAttackByWhite = true;
                                    }
                                }
                            }
                        }
                        if (Spaces[i][j].Piece is not null &&
                            Spaces[i][j].Piece!.GetBelongsTo() == Player.Black)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int m = 0; m < 8; m++)
                                {
                                    if (Spaces[i][j].Piece is not null &&
                                        (Spaces![i][j].Piece!
                                        .CanLegallyTryToMoveFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                        !(Spaces![i][j].Piece!
                                        .IsBlocked(Spaces[i][j], Spaces[k][m])))
                                    {
                                        Spaces[k][m].IsUnderAttackByBlack = true;
                                    }
                                    if (Spaces[i][j].Piece is not null &&
                                        (Spaces[i][j].Piece!
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
        }

        /// <summary>
        /// Tests if either Player's KingSpace is attacked by the opponent.
        /// Used to determine if a move just made puts your own king in check
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Checks for White being Checkmated
        /// </summary>
        /// <returns></returns>
        public static bool WhiteIsCheckMated()
        {
            if (Turn == Player.White)
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
                                    Spaces![i][j].Piece!.GetBelongsTo() == Player.White)
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
                                                //Console.WriteLine("White is not checkmated!");
                                                return false;
                                            }
                                        }
                                        if ((Spaces![i][j].Piece!
                                            .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                            !(Spaces![i][j].Piece!.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                        {
                                            if (Spaces[i][j].Piece!.TryCapture(Spaces[i][j], Spaces[k][m]))
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
                    Console.ReadLine();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for White being Stalemated
        /// </summary>
        /// <returns></returns>
        public static bool WhiteIsStaleMated()
        {
            if (Turn == Player.White)
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
                    Console.ReadLine();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for Black being Checkmated
        /// </summary>
        /// <returns></returns>
        public static bool BlackIsCheckMated()
        {
            if (Turn == Player.Black)
            {
                FindAllSpacesAttacked();
                if (BlackKingSpace!.IsUnderAttackByWhite)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {

                            if (Spaces![i][j].Piece is not null &&
                                Spaces![i][j].Piece!.GetBelongsTo() == Player.Black)
                            {
                                for (int k = 0; k < 8; k++)
                                {
                                    for (int m = 0; m < 8; m++)
                                    {
                                        if (Spaces![i][j].Piece!
                                            .CanLegallyTryToMoveFromSpaceToSpace(Spaces[i][j], Spaces[k][m]) &&
                                            !(Spaces![i][j].Piece!.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                        {
                                            if (Spaces[i][j].Piece!.TryMove(Spaces[i][j], Spaces[k][m]))
                                            {
                                                //Console.WriteLine("Black is not checkmated!");
                                                return false;
                                            }
                                        }
                                        if ((Spaces![i][j].Piece!
                                            .CanLegallyTryToCaptureFromSpaceToSpace(Spaces[i][j], Spaces[k][m])) &&
                                            !(Spaces![i][j].Piece!.IsBlocked(Spaces[i][j], Spaces[k][m])))
                                        {
                                            if (Spaces[i][j].Piece!.TryCapture(Spaces[i][j], Spaces[k][m]))
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
                    Console.ReadLine();
                    return true;
                }
            }
            //Console.WriteLine("Black is not in check!");
            return false;
        }

        /// <summary>
        /// Checks for Black being Stalekmated
        /// </summary>
        /// <returns></returns>
        public static bool BlackIsStaleMated()
        {
            if (Turn == Player.Black)
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
                    Console.ReadLine();
                    return true;
                }
            }
            return false;
        }
    }
}
