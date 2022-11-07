using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    space.piece = new Piece(name: "");
                    spaces[i][j] = space;
                }
            }

            spaces[0][0].piece.Name = "r";
            spaces[0][1].piece.Name = "n";
            spaces[0][2].piece.Name = "b";
            spaces[0][3].piece.Name = "k";
            spaces[0][4].piece.Name = "q";
            spaces[0][5].piece.Name = "b";
            spaces[0][6].piece.Name = "n";
            spaces[0][7].piece.Name = "r";

            for (int i = 0; i < 8; i++)
            {
                spaces[1][i].piece.Name = "p";
            }

            for (int i = 0; i < 8; i++)
            {
                spaces[6][i].piece.Name = "P";
            }

            spaces[7][0].piece.Name = "R";
            spaces[7][1].piece.Name = "N";
            spaces[7][2].piece.Name = "B";
            spaces[7][3].piece.Name = "K";
            spaces[7][4].piece.Name = "Q";
            spaces[7][5].piece.Name = "B";
            spaces[7][6].piece.Name = "N";
            spaces[7][7].piece.Name = "R";

            PrintBoard();
        }

        public void PrintBoard()
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(spaces[i][j].piece.Name);
                }
                Console.WriteLine();
            }
        }
    }
}
