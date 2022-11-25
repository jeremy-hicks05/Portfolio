using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    /*  TODO: 
     * 1. Enable castling K side
     * 2. Enable castling Q side
     */
    internal class King : Piece
    {
        bool hasMoved;
        public King(string name, Player belongsTo) : base(name, belongsTo)
        {
            hasMoved = false;
        }

        //public override void MoveTo(Space spaceMovedTo)
        //{
        //    // move like a king
        //    spaceMovedTo.Piece.Name = Name;
        //    Name = "[ ]";
        //    hasMoved = false;
        //}

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // if neither king nor king side rook has moved, and toSpace is a specific spot
            // and the king will not 'move through check' or end up in check
            // perform a castle

            // if king hasn't moved - perform castle king side for black
            if((!hasMoved && belongsToPlayer == Player.Black) && (fromSpace.X == toSpace.X) && toSpace.Y - fromSpace.Y == 2)
            {
                // perform castle
                Board.CastleKingSideBlack();
                return false;
                //return true;
            }

            else if ((!hasMoved && belongsToPlayer == Player.Black) && (fromSpace.X == toSpace.X) && toSpace.Y + fromSpace.Y == 2)
            {
                // perform castle
                Board.CastleQueenSideBlack();
                return false;
                //return true;
            }

            else if ((!hasMoved && belongsToPlayer == Player.White) && (fromSpace.X == toSpace.X) && fromSpace.Y - toSpace.Y == 2)
            {
                // perform castle
                Board.CastleQueenSideWhite();
                return false;
                //return true;
            }

            else if ((!hasMoved && belongsToPlayer == Player.Black) && (fromSpace.X == toSpace.X) && fromSpace.Y - toSpace.Y == 2)
            {
                // perform castle
                Board.CastleKingSideWhite();
                return false;
                //return true;
            }

            if (toSpace.X - fromSpace.X > 1 || toSpace.Y - fromSpace.Y > 1)
            {
                return false;
            }
            else if (toSpace.Piece?.belongsToPlayer == belongsToPlayer)
            {
                return false;
            }
            hasMoved = true;
            return true;
        }
    }
}
