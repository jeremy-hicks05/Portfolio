using ConsoleChessV3.Pieces.Black;
using ConsoleChessV3.Pieces.White;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleChessV3.ChessMoves.Subclasses
{
    internal class Promotion : ChessMove
    {
        public Promotion(Space startingSpace, Space endingSpace) : base(startingSpace, endingSpace)
        {

        }

        public override void Perform()
        {
            string? promotionChoice = "Z";
            Console.WriteLine("Promotion!");
            while (!Regex.Match(promotionChoice, "[NBRQ]").Success)
            {
                Console.WriteLine("Which piece would you like to promote to?");
                Console.WriteLine("N: Knight");
                Console.WriteLine("B: Bishop");
                Console.WriteLine("R: Rook");
                Console.WriteLine("Q: Queen");
                promotionChoice = Console.ReadLine()!.ToUpper();

                if (ChessBoard.Turn == Enums.Player.White)
                {
                    switch (promotionChoice)
                    {
                        case "N":
                            TargetSpace.Piece = new WhiteKnight();
                            break;
                        case "B":
                            TargetSpace.Piece = new WhiteBishop();
                            break;
                        case "R":
                            TargetSpace.Piece = new WhiteRook();
                            break;
                        case "Q":
                            TargetSpace.Piece = new WhiteQueen();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (promotionChoice)
                    {
                        case "N":
                            TargetSpace.Piece = new BlackKnight();
                            break;
                        case "B":
                            TargetSpace.Piece = new BlackBishop();
                            break;
                        case "R":
                            TargetSpace.Piece = new BlackRook();
                            break;
                        case "Q":
                            TargetSpace.Piece = new BlackQueen();
                            break;
                        default:
                            break;
                    }
                }
                ChessBoard.PrintBoard();
            }
            StartingSpace.Clear();
        }

        public override void Reverse()
        {
            // restore pawn and remove promoted piece
            StartingSpace.Piece = StartingPiece;
            StartingPiece.SetHasMoved(StartingPieceHasMoved);

            TargetSpace.Piece = TargetPiece;
            if (TargetPiece is not null)
            {
                TargetPiece.SetHasMoved(TargetPieceHasMoved);
            }
        }

        public override bool IsValidChessMove()
        {
            return base.IsValidChessMove();
        }
    }
}
