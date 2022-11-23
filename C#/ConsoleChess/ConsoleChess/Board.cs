using ConsoleChess.Interfaces;
using ConsoleChess.Pieces;

namespace ConsoleChess
{
    internal class Board
    {
        public Space[][] spaces = new Space[8][];

        public Board()
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
                    space.Latitude = i;
                    space.Longitude = j;
                    space.Piece = new Piece(name: "[ ]");
                    spaces[i][j] = space;
                }
            }

            spaces[0][0].Piece = new Rook("[r]");
            spaces[0][1].Piece = new Knight("[n]");
            spaces[0][2].Piece = new Bishop("[b]");
            spaces[0][3].Piece = new King("[k]");
            spaces[0][4].Piece = new Queen("[q]");
            spaces[0][5].Piece = new Bishop("[b]");
            spaces[0][6].Piece = new Knight("[n]");
            spaces[0][7].Piece = new Rook("[r]");

            for (int i = 0; i < 8; i++)
            {
                spaces[1][i].Piece = new Pawn("[p]");
            }

            for (int i = 0; i < 8; i++)
            {
                spaces[6][i].Piece = new Pawn("[P]");
            }

            spaces[7][0].Piece = new Rook("[R]");
            spaces[7][1].Piece = new Knight("[N]");
            spaces[7][2].Piece = new Bishop("[B]");
            spaces[7][3].Piece = new King("[K]");
            spaces[7][4].Piece = new Queen("[Q]");
            spaces[7][5].Piece = new Bishop("[B]");
            spaces[7][6].Piece = new Knight("[N]");
            spaces[7][7].Piece = new Rook("[R]");

            PrintBoard();
        }

        public void PrintBoard()
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

        public void MovePieceFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            toSpace.Piece = fromSpace.Piece;
            fromSpace.Piece = new Piece("[ ]");
            
                
            //fromSpace.Piece.MoveTo(toSpace);
        }
    }
}
