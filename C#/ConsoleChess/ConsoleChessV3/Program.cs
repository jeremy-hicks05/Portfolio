﻿namespace ConsoleChessV3
{
    using static ChessBoard;
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Chess!");
            Console.ReadLine();
            InitBoard();

            while (true)
            {
                BlackIsCheckMated();
                WhiteIsCheckMated();
                BlackIsStaleMated();
                WhiteIsStaleMated();
                PrintBoard();

                GetInitialSpaceInput();
                GetTargetSpaceInput();

                PlayMove();

                //PrintBoard();

                //ShowMoveHistory();
                
                //PrintBoard();
                //ShowMoveHistory();
                //Console.ReadLine();
            }
            //Console.WriteLine("Exiting Chess");
        }
    }
}