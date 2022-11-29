using ConsoleChess.Interfaces;
using ConsoleChess.Enums;

namespace ConsoleChess.Pieces
{
    internal class King : Piece
    {
        public King(string name, Player belongsTo) : base(name, belongsTo)
        {
            hasMoved = false;
        }

        public override bool CanTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // test whether attempted move follows the rules of chess
            if (fromSpace == toSpace)
            {
                return false;
            }
            if (Math.Abs(toSpace.X - fromSpace.X) <= 1 && Math.Abs(toSpace.Y - fromSpace.Y) <= 1)
            {
                return true;
            }

            // check for castle attempt
            // castling move(s)
            if (fromSpace == Board.spaces[Board.NotationToInt("1")][Board.NotationToInt("E")] &&
                toSpace == Board.spaces[Board.NotationToInt("1")][Board.NotationToInt("G")])
            {
                return true;
            }

            if (fromSpace == Board.spaces[Board.NotationToInt("8")][Board.NotationToInt("E")] &&
                toSpace == Board.spaces[Board.NotationToInt("8")][Board.NotationToInt("G")])
            {
                return true;
            }

            if (fromSpace == Board.spaces[Board.NotationToInt("1")][Board.NotationToInt("E")] &&
                toSpace == Board.spaces[Board.NotationToInt("1")][Board.NotationToInt("C")])
            {
                return true;
            }

            if (fromSpace == Board.spaces[Board.NotationToInt("8")][Board.NotationToInt("E")] &&
                toSpace == Board.spaces[Board.NotationToInt("8")][Board.NotationToInt("C")])
            {
                return true;
            }

            return false;
        }

        public override bool HasPiecesBlockingMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            // standard move
            if (toSpace.Piece.belongsToPlayer == belongsToPlayer)
            {
                // is blocked
                return true;
            }

            // fallout false
            return false;
        }

        public override bool CanTryToCapture(Space fromSpace, Space toSpace)
        {
            if (fromSpace == toSpace)
            {
                return false;
            }
            else if (Math.Abs(toSpace.X - fromSpace.X) <= 1 && Math.Abs(toSpace.Y - fromSpace.Y) <= 1)
            {
                return true;
            }

            // if king hasn't moved - perform castle king side for black
            if (!Board.BlackKingIsInCheck() && 
                (!hasMoved && belongsToPlayer == Player.Black) &&
                (fromSpace.X == toSpace.X) &&
                toSpace.Y - fromSpace.Y == 2) // TODO: check if rook has moved
            {
                // attempt castle
                return true;
            }
            else if (!Board.BlackKingIsInCheck() &&
                    (!hasMoved && belongsToPlayer == Player.Black) &&
                    (fromSpace.X == toSpace.X) && 
                    fromSpace.Y - toSpace.Y == 2) // TODO: check if rook has moved
            {
                // attempt castle
                return true;
            }
            else if (!Board.WhiteKingIsInCheck() &&
                    (!hasMoved && belongsToPlayer == Player.White) &&
                    (fromSpace.X == toSpace.X) && 
                    fromSpace.Y - toSpace.Y == 2) // TODO: check if rook has moved
            {
                // attempt castle
                return true;
            }
            else if (!Board.WhiteKingIsInCheck() &&
                    (!hasMoved && belongsToPlayer == Player.White) &&
                    (fromSpace.X == toSpace.X) &&
                    toSpace.Y - fromSpace.Y == 2) // TODO: check if rook has moved
            {
                // attempt castle
                return true;
            }

            // fallout false
            return false;
        }

        public override bool CanMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (toSpace.Piece.belongsToPlayer == belongsToPlayer)
            {
                return false;
            }

            //if space is being attacked by opposition, do not move there
            if (belongsToPlayer == Player.Black && toSpace.IsUnderAttackByWhite)
            {
                return false;
            }
            else if (belongsToPlayer == Player.White && toSpace.IsUnderAttackByBlack)
            {
                return false;
            }

            // if neither king nor king side rook has moved, and toSpace is a specific spot
            // and the king will not 'move through check' or end up in check
            // perform a castle

            // if king hasn't moved - perform castle king side for black
            if (!Board.BlackKingIsInCheck() && (!hasMoved && belongsToPlayer == Player.Black) && 
                (fromSpace.X == toSpace.X) && 
                toSpace.Y - fromSpace.Y == 2)
            {
                // perform castle
                Board.CastleKingSideBlack();
                return false;
                //return true;
            }

            else if (!Board.BlackKingIsInCheck() && 
                    (!hasMoved && belongsToPlayer == Player.Black) && 
                    (fromSpace.X == toSpace.X) && fromSpace.Y - toSpace.Y == 2)
            {
                // perform castle
                Board.CastleQueenSideBlack();
                return false;
                //return true;
            }

            else if (!Board.WhiteKingIsInCheck() && 
                    (!hasMoved && belongsToPlayer == Player.White) && 
                    (fromSpace.X == toSpace.X) && fromSpace.Y - toSpace.Y == 2)
            {
                // perform castle
                Board.CastleQueenSideWhite();
                return false;
                //return true;
            }
            else if (!Board.WhiteKingIsInCheck() && 
                    (!hasMoved && belongsToPlayer == Player.White) && 
                    (fromSpace.X == toSpace.X) && 
                    toSpace.Y - fromSpace.Y == 2)
            {
                // perform castle
                Board.CastleKingSideWhite();
                return false;
                //return true;
            }

            if (Math.Abs(toSpace.X - fromSpace.X) > 1 || Math.Abs(toSpace.Y - fromSpace.Y) > 1)
            {
                return false;
            }
            
            return true;
        }
    }
}
