using ConsoleChess.Interfaces;
using ConsoleChess.Pieces;
using ConsoleChess.Players;

namespace ConsoleChess
{
    internal static class Board
    {
        public static Space[][] spaces { get; set; } = new Space[8][];

        //public Board()
        //{
        //    spaces[0][7].Piece = new Rook("[r]", Player.Black);

        //    for (int i = 0; i < 8; i++)
        //    {
        //        spaces[1][i].Piece = new Pawn("[p]", Player.Black);
        //    }

        //    for (int i = 0; i < 8; i++)
        //    {
        //        spaces[6][i].Piece = new Pawn("[P]", Player.White);
        //    }

        //    spaces[7][0].Piece = new Rook("[R]", Player.White);
        //    spaces[7][1].Piece = new Knight("[N]", Player.White);
        //    spaces[7][2].Piece = new Bishop("[B]", Player.White);
        //    spaces[7][3].Piece = new King("[K]", Player.White);
        //    spaces[7][4].Piece = new Queen("[Q]", Player.White);
        //    spaces[7][5].Piece = new Bishop("[B]", Player.White);
        //    spaces[7][6].Piece = new Knight("[N]", Player.White);
        //    spaces[7][7].Piece = new Rook("[R]", Player.White);

        //    PrintBoard();
        //}

        public static void InitBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                spaces[i] = new Space[8];
                for (int j = 0; j < 8; j++)
                {
                    spaces[i][j] = new Space();
                }
            }

            for (int i = 0; i < 8; i ++)
            {
                for(int j = 0; j < 8; j ++)
                {
                    Space space = new Space();
                    space.X = i;
                    space.Y = j;
                    space.Piece = new Piece(name: "[ ]", Player.None);
                    spaces[i][j] = space;
                }
            }

            spaces[0][0].Piece = new Rook("[r]", Player.Black);
            spaces[0][1].Piece = new Knight("[n]", Player.Black);
            spaces[0][2].Piece = new Bishop("[b]", Player.Black);
            spaces[0][3].Piece = new King("[k]", Player.Black);
            spaces[0][4].Piece = new Queen("[q]", Player.Black);
            spaces[0][5].Piece = new Bishop("[b]", Player.Black);
            spaces[0][6].Piece = new Knight("[n]", Player.Black);
            spaces[0][7].Piece = new Rook("[r]", Player.Black);

            for (int i = 0; i < 8; i++)
            {
                spaces[1][i].Piece = new Pawn("[p]", Player.Black);
            }

            for (int i = 0; i < 8; i++)
            {
                spaces[6][i].Piece = new Pawn("[P]", Player.White);
            }

            spaces[7][0].Piece = new Rook("[R]", Player.White);
            spaces[7][1].Piece = new Knight("[N]", Player.White);
            spaces[7][2].Piece = new Bishop("[B]", Player.White);
            spaces[7][3].Piece = new King("[K]", Player.White);
            spaces[7][4].Piece = new Queen("[Q]", Player.White);
            spaces[7][5].Piece = new Bishop("[B]", Player.White);
            spaces[7][6].Piece = new Knight("[N]", Player.White);
            spaces[7][7].Piece = new Rook("[R]", Player.White);

            PrintBoard();
        }

        public static void PrintBoard()
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(spaces[i][j].Piece.Name);
                }
                Console.WriteLine();
            }
        }

        public static void MovePieceFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            toSpace.Piece = fromSpace.Piece;
            fromSpace.Piece = new Piece("[ ]", Player.None);
        }
    }
}
