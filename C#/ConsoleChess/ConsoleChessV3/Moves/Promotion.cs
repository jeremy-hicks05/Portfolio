using ConsoleChessV3.Pieces.Black;
using ConsoleChessV3.Pieces.White;
using ConsoleChessV3.SuperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessV3.Moves
{
    internal class Promotion : ChessMove
    {
        public Promotion(Space startingSpace, Space endingSpace) : base(startingSpace, endingSpace)
        {

        }

        public override void Perform()
        {
            Console.WriteLine("Promotion!");
            Console.WriteLine("Which piece would you like to promote to?");
            Console.WriteLine("N: Knight");
            Console.WriteLine("B: Bishop");
            Console.WriteLine("R: Rook");
            Console.WriteLine("Q: Queen");
            string? promotionChoice = Console.ReadLine()!.ToUpper();

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
                    default:
                        TargetSpace.Piece = new WhiteQueen();
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
                    default:
                        TargetSpace.Piece = new BlackQueen();
                        break;
                }
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

            if (RestoreSpace is not null)
            {
                RestoreSpace.Piece = RestorePiece;
                if (RestoreSpace.Piece is not null)
                {
                    RestoreSpace.Piece.SetHasMoved(RestorePieceHasMoved);
                }
            }
        }

        public override bool IsValidChessMove()
        {
            return base.IsValidChessMove();
        }
    }
}
