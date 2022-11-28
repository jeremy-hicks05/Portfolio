using ConsoleChess.Interfaces;
using ConsoleChess.Pieces;
using ConsoleChess.Enums;

namespace ConsoleChess
{
    internal static class Board
    {
        public static Space[][] spaces { get; set; } = new Space[8][];
        public static Player turn;
        public static Space? WhiteKingSpace;
        public static Space? BlackKingSpace;

        public static void InitBoard()
        {

            // Build 2D Spaces Array
            for (int i = 0; i < 8; i++)
            {
                spaces[i] = new Space[8];
                for (int j = 0; j < 8; j++)
                {
                    spaces[i][j] = new Space();
                }
            }

            // Populate empty spaces, declare X and Y coordinates for each space
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Space space = new Space();
                    space.X = i;
                    space.Y = j;
                    space.Piece = new Piece(name: "[ ]", Player.None);
                    spaces[i][j] = space;
                }
            }

            // populate Black back row
            spaces[0][0].Piece = new Rook("[r]", Player.Black);
            spaces[0][1].Piece = new Knight("[n]", Player.Black);
            spaces[0][2].Piece = new Bishop("[b]", Player.Black);
            spaces[0][3].Piece = new Queen("[q]", Player.Black);
            spaces[0][4].Piece = new King("[k]", Player.Black);
            spaces[0][5].Piece = new Bishop("[b]", Player.Black);
            spaces[0][6].Piece = new Knight("[n]", Player.Black);
            spaces[0][7].Piece = new Rook("[r]", Player.Black);

            BlackKingSpace = spaces[0][4];

            // populate Black pawns
            for (int i = 0; i < 8; i++)
            {
                spaces[1][i].Piece = new Pawn("[p]", Player.Black);
            }

            // populate White pawns
            for (int i = 0; i < 8; i++)
            {
                spaces[6][i].Piece = new Pawn("[P]", Player.White);
            }

            // populate White back row
            spaces[7][0].Piece = new Rook("[R]", Player.White);
            spaces[7][1].Piece = new Knight("[N]", Player.White);
            spaces[7][2].Piece = new Bishop("[B]", Player.White);
            spaces[7][3].Piece = new Queen("[Q]", Player.White);
            spaces[7][4].Piece = new King("[K]", Player.White);
            spaces[7][5].Piece = new Bishop("[B]", Player.White);
            spaces[7][6].Piece = new Knight("[N]", Player.White);
            spaces[7][7].Piece = new Rook("[R]", Player.White);

            WhiteKingSpace = spaces[7][4];

            FindAllSpacesAttacked();

            PrintBoard();
            turn = Player.White;
        }

        public static void PrintBoard()
        {
            Console.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(8 - i);
                    }
                    Console.Write(spaces[i][j].Piece.Name);
                }

                Console.WriteLine();
            }
            Console.WriteLine("  A  B  C  D  E  F  G  H");
        }

        public static void FindAllSpacesAttacked()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // reset being attacked flags
                    spaces[i][j].IsUnderAttackByBlack = false;
                    spaces[i][j].IsUnderAttackByWhite = false;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (spaces[i][j].Piece.belongsToPlayer == Player.White)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            for (int m = 0; m < 8; m++)
                            {
                                if (spaces[i][j].Piece.CanAttackSpace(spaces[i][j], spaces[k][m]))
                                {
                                    spaces[k][m].IsUnderAttackByWhite = true;
                                }
                            }
                        }
                    }

                    if (spaces[i][j].Piece.belongsToPlayer == Player.Black)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            for (int m = 0; m < 8; m++)
                            {
                                if (spaces[i][j].Piece.CanAttackSpace(spaces[i][j], spaces[k][m]))
                                {
                                    spaces[k][m].IsUnderAttackByBlack = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void MovePieceFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (fromSpace.Piece.belongsToPlayer == turn)
            {
                // "copy" piece from starting space to ending space
                // then test conditions to check for
                // promotion
                // king check
                //
                // if king is in check, "undo move" and keep fromSpace, clear toSpace
                toSpace.Piece = fromSpace.Piece;

                // check if piece is white pawn on row 8 - pawn promotion
                if (fromSpace.Piece.GetType() == typeof(Pawn) && fromSpace.Piece.belongsToPlayer == Player.White && toSpace.X == 0)
                {
                    // offer piece selection and transform into selected piece
                    Console.WriteLine("Promotion!");
                    Console.WriteLine("Select a piece to promote to:");
                    Console.WriteLine("N: Knight");
                    Console.WriteLine("B: Bishop");
                    Console.WriteLine("R: Rook");
                    Console.WriteLine("Q: Queen");
                    string? promotionSelection = Console.ReadLine();

                    switch (promotionSelection)
                    {
                        case "N":
                            toSpace.Piece = new Knight("[N]", Player.White);
                            break;
                        case "B":
                            toSpace.Piece = new Bishop("[B]", Player.White);
                            break;
                        case "R":
                            toSpace.Piece = new Rook("[R]", Player.White);
                            break;
                        case "Q":
                            toSpace.Piece = new Queen("[Q]", Player.White);
                            break;
                    }
                }
                // check if piece is black pawn on row 8
                else if (fromSpace.Piece.GetType() == typeof(Pawn) && fromSpace.Piece.belongsToPlayer == Player.Black && toSpace.X == 7)
                {
                    // offer piece selection and transform into selected piece
                    Console.WriteLine("Promotion!");
                    Console.WriteLine("Select a piece to promote to:");
                    Console.WriteLine("N: Knight");
                    Console.WriteLine("B: Bishop");
                    Console.WriteLine("R: Rook");
                    Console.WriteLine("Q: Queen");
                    string? promotionSelection = Console.ReadLine();

                    switch (promotionSelection)
                    {
                        case "N":
                            toSpace.Piece = new Knight("[n]", Player.Black);
                            break;
                        case "B":
                            toSpace.Piece = new Bishop("[b]", Player.Black);
                            break;
                        case "R":
                            toSpace.Piece = new Rook("[r]", Player.Black);
                            break;
                        case "Q":
                            toSpace.Piece = new Queen("[q]", Player.Black);
                            break;
                    }
                } // end pawn promotion

                // check if King is the piece being moved
                //if (fromSpace.Piece.GetType() == typeof(King) && turn == Player.White)
                //{
                //    // update King space for White
                //    WhiteKingSpace = toSpace;
                //}
                //else if (fromSpace.Piece.GetType() == typeof(King) && turn == Player.Black)
                //{
                //    // update King space for Black
                //    BlackKingSpace = toSpace;
                //}

                //if (BlackKingSpace is not null && BlackKingSpace.IsUnderAttackByWhite)
                //{
                //    // cancel move
                //    toSpace.Piece = new Piece("[ ]", Player.None);
                //}
                //else if (WhiteKingSpace is not null && WhiteKingSpace.IsUnderAttackByBlack)
                //{
                //    // cancel move
                //    toSpace.Piece = new Piece("[ ]", Player.None);
                //}
                else
                {
                    // try moving the piece
                    Piece tempFromPiece = fromSpace.Piece;

                    if (fromSpace.Piece.CanAttackSpace(fromSpace, toSpace))
                    {
                        fromSpace.Piece = new Piece("[ ]", Player.None);

                        // test if still in check after toSpace is also gone
                        Piece tempToSpacePiece = toSpace.Piece;
                        toSpace.Piece = new Piece("[ ]", Player.None);

                        FindAllSpacesAttacked();

                        if (turn == Player.White && WhiteKingIsInCheck())
                        {
                            // cancel move
                            toSpace.Piece = tempToSpacePiece;
                            fromSpace.Piece = tempFromPiece;
                        }
                        else if (turn == Player.Black && BlackKingIsInCheck())
                        {
                            // cancel move
                            toSpace.Piece = tempToSpacePiece;
                            fromSpace.Piece = tempFromPiece;
                        }
                        else
                        {
                            // successful move
                            toSpace.Piece.hasMoved = true;
                            toSpace.Piece = tempToSpacePiece;

                            // change turns
                            if (turn == Player.White)
                            {
                                turn = Player.Black;
                            }
                            else
                            {
                                turn = Player.White;
                            }
                        }
                    }
                    else
                    {
                        fromSpace.Piece = new Piece("[ ]", Player.None);
                        toSpace.Piece = tempFromPiece;

                        FindAllSpacesAttacked();

                        if (turn == Player.White && WhiteKingIsInCheck())
                        {
                            // cancel move
                            toSpace.Piece = new Piece("[ ]", Player.None);
                            fromSpace.Piece = tempFromPiece;
                        }
                        else if (turn == Player.Black && BlackKingIsInCheck())
                        {
                            // cancel move
                            toSpace.Piece = new Piece("[ ]", Player.None);
                            fromSpace.Piece = tempFromPiece;
                        }
                        else
                        {
                            // successful move
                            toSpace.Piece.hasMoved = true;

                            // change turns
                            if (turn == Player.White)
                            {
                                turn = Player.Black;
                            }
                            else
                            {
                                turn = Player.White;
                            }
                        }
                    }
                }
            }
        }

        public static int NotationToInt(string? notation)
        {
            int translatedNotaion = -1;

            if (notation is not null)
            {

                switch (notation.ToUpper())
                {
                    case "A":
                    case "8":
                        translatedNotaion = 0;
                        break;
                    case "B":
                    case "7":
                        translatedNotaion = 1;
                        break;
                    case "C":
                    case "6":
                        translatedNotaion = 2;
                        break;
                    case "D":
                    case "5":
                        translatedNotaion = 3;
                        break;
                    case "E":
                    case "4":
                        translatedNotaion = 4;
                        break;
                    case "F":
                    case "3":
                        translatedNotaion = 5;
                        break;
                    case "2":
                    case "G":
                        translatedNotaion = 6;
                        break;
                    case "H":
                    case "1":
                        translatedNotaion = 7;
                        break;
                }
            }
            return translatedNotaion;
        }

        public static void CastleKingSideWhite()
        {
            //testing
            if (spaces[NotationToInt("1")][NotationToInt("F")].Piece.belongsToPlayer == Player.None &&
                spaces[NotationToInt("1")][NotationToInt("G")].Piece.belongsToPlayer == Player.None)
            {
                Rook myRook = (Rook)spaces[NotationToInt("1")][NotationToInt("H")].Piece;
                King myKing = (King)spaces[NotationToInt("1")][NotationToInt("E")].Piece;
                if (!myRook.hasMoved && !myKing.hasMoved)
                {
                    // check if spaces between the k and r are attacked by black
                    if (spaces[NotationToInt("1")][NotationToInt("F")].IsUnderAttackByBlack == false &&
                        spaces[NotationToInt("1")][NotationToInt("G")].IsUnderAttackByBlack == false)
                    {
                        spaces[NotationToInt("1")][NotationToInt("E")].Piece = new Piece("[ ]", Player.None);
                        spaces[NotationToInt("1")][NotationToInt("H")].Piece = new Piece("[ ]", Player.None);

                        spaces[NotationToInt("1")][NotationToInt("G")].Piece = new King("[K]", Player.White);
                        spaces[NotationToInt("1")][NotationToInt("F")].Piece = new Rook("[R]", Player.White);

                        WhiteKingSpace = spaces[NotationToInt("1")][NotationToInt("G")];
                    }
                }
            }
        }

        public static void CastleQueenSideWhite()
        {
            // works
            if (spaces[NotationToInt("1")][NotationToInt("B")].Piece.belongsToPlayer == Player.None &&
                spaces[NotationToInt("1")][NotationToInt("C")].Piece.belongsToPlayer == Player.None &&
                spaces[NotationToInt("1")][NotationToInt("D")].Piece.belongsToPlayer == Player.None)
            {
                Rook myRook = (Rook)spaces[NotationToInt("1")][NotationToInt("A")].Piece;
                King myKing = (King)spaces[NotationToInt("1")][NotationToInt("E")].Piece;
                if (!myRook.hasMoved && !myKing.hasMoved)
                {
                    // check if spaces between the k and r are attacked by black
                    if (spaces[NotationToInt("1")][NotationToInt("C")].IsUnderAttackByBlack == false &&
                        spaces[NotationToInt("1")][NotationToInt("D")].IsUnderAttackByBlack == false)
                    {
                        spaces[NotationToInt("1")][NotationToInt("A")].Piece = new Piece("[ ]", Player.None);
                        spaces[NotationToInt("1")][NotationToInt("E")].Piece = new Piece("[ ]", Player.None);

                        spaces[NotationToInt("1")][NotationToInt("C")].Piece = new King("[K]", Player.Black);
                        spaces[NotationToInt("1")][NotationToInt("D")].Piece = new Rook("[R]", Player.Black);

                        WhiteKingSpace = spaces[NotationToInt("1")][NotationToInt("C")];
                    }
                }
            }
        }

        public static void CastleKingSideBlack()
        {
            // works
            if (spaces[NotationToInt("8")][NotationToInt("F")].Piece.belongsToPlayer == Player.None &&
                spaces[NotationToInt("8")][NotationToInt("G")].Piece.belongsToPlayer == Player.None)
            {
                Rook myRook = (Rook)spaces[NotationToInt("8")][NotationToInt("H")].Piece;
                King myKing = (King)spaces[NotationToInt("8")][NotationToInt("E")].Piece;
                if (!myRook.hasMoved && !myKing.hasMoved)
                {
                    // check if spaces between the k and r are attacked by white
                    if (spaces[NotationToInt("8")][NotationToInt("F")].IsUnderAttackByWhite == false &&
                        spaces[NotationToInt("8")][NotationToInt("G")].IsUnderAttackByWhite == false)
                    {
                        spaces[NotationToInt("8")][NotationToInt("E")].Piece = new Piece("[ ]", Player.None);
                        spaces[NotationToInt("8")][NotationToInt("H")].Piece = new Piece("[ ]", Player.None);

                        spaces[NotationToInt("8")][NotationToInt("G")].Piece = new King("[k]", Player.Black);
                        spaces[NotationToInt("8")][NotationToInt("F")].Piece = new Rook("[r]", Player.Black);

                        BlackKingSpace = spaces[NotationToInt("8")][NotationToInt("G")];
                    }
                }
            }
        }

        public static void CastleQueenSideBlack()
        {
            // works
            if (spaces[NotationToInt("8")][NotationToInt("B")].Piece.belongsToPlayer == Player.None &&
                spaces[NotationToInt("8")][NotationToInt("C")].Piece.belongsToPlayer == Player.None &&
                spaces[NotationToInt("8")][NotationToInt("D")].Piece.belongsToPlayer == Player.None)
            {
                Rook myRook = (Rook)spaces[NotationToInt("8")][NotationToInt("A")].Piece;
                King myKing = (King)spaces[NotationToInt("8")][NotationToInt("E")].Piece;
                if (!myRook.hasMoved && !myKing.hasMoved)
                {
                    // check if spaces between the k and r are attacked by white
                    if (spaces[NotationToInt("8")][NotationToInt("C")].IsUnderAttackByWhite == false &&
                        spaces[NotationToInt("8")][NotationToInt("D")].IsUnderAttackByWhite == false)
                    {
                        spaces[NotationToInt("8")][NotationToInt("E")].Piece = new Piece("[ ]", Player.None);
                        spaces[NotationToInt("8")][NotationToInt("A")].Piece = new Piece("[ ]", Player.None);

                        spaces[NotationToInt("8")][NotationToInt("C")].Piece = new King("[k]", Player.Black);
                        spaces[NotationToInt("8")][NotationToInt("D")].Piece = new Rook("[r]", Player.Black);

                        BlackKingSpace = spaces[NotationToInt("8")][NotationToInt("C")];
                    }
                }
            }
        }

        public static bool WhiteKingIsInCheck()
        {
            // if white king's space is under attack by black
            if (WhiteKingSpace is not null && WhiteKingSpace.IsUnderAttackByBlack)
            {
                Console.WriteLine("White king in check!");
                return true;
            }
            return false;
        }

        public static bool BlackKingIsInCheck()
        {
            // if black king's space is under attack by white
            if (BlackKingSpace is not null && BlackKingSpace.IsUnderAttackByWhite)
            {
                Console.WriteLine("Black king in check!");
                return true;
            }
            return false;
        }
    }
}
