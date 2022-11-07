using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess
{
    public class Board
    {
        static List<Space> _spaces;

        public Board()
        {
            _spaces = new List<Space>
            {
                new Space
                {
                    Latitude=0,
                    Longitude=0,
                    piece = new Piece(name: "r")
                }
            };

            PrintBoard();
        }

        public void PrintBoard()
        {
            foreach(Space s in _spaces)
            {
                Console.WriteLine(s.piece.Name);
            }
            //Console.WriteLine("_________________________");
            //Console.WriteLine("|  |  |  |  |  |  |  |  |");
            //Console.WriteLine("+--+--+--+--+--+--+--+--+");
            //Console.WriteLine("|  |  |  |  |  |  |  |  |");
            //Console.WriteLine("+--+--+--+--+--+--+--+--+");
            //Console.WriteLine("|  |  |  |  |  |  |  |  |");
            //Console.WriteLine("+--+--+--+--+--+--+--+--+");
            //Console.WriteLine("|  |  |  |  |  |  |  |  |");
            //Console.WriteLine("+--+--+--+--+--+--+--+--+");
            //Console.WriteLine("|  |  |  |  |  |  |  |  |");
            //Console.WriteLine("+--+--+--+--+--+--+--+--+");
            //Console.WriteLine("|  |  |  |  |  |  |  |  |");
            //Console.WriteLine("+--+--+--+--+--+--+--+--+");
            //Console.WriteLine("|  |  |  |  |  |  |  |  |");
            //Console.WriteLine("+--+--+--+--+--+--+--+--+");
            //Console.WriteLine("|  |  |  |  |  |  |  |  |");
            //Console.WriteLine("-------------------------");
        }
    }
}
