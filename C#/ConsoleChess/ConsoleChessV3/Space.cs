using ConsoleChessV3.Enums;
using ConsoleChessV3.Interfaces;

namespace ConsoleChessV3
{
    internal class Space
    {
        public int Column;
        public int Row;
        public IPiece? Piece;

        public bool IsUnderAttackByWhite;
        public bool IsUnderAttackByBlack;
        public Space()
        {
            Column = -1;
            Row = -1;
            Piece = null;
        }

        public override string ToString()
        {
            return (Piece == null ? $"[ ]" : $"[{Piece.GetName()}]");
        }

        public void Clear()
        {
            this.Piece = null;
        }

        /// <summary>
        /// Returns whether Space has a piece on it
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return Piece == null;
        }

        /// <summary>
        /// Returns whether Space has a piece on it
        /// </summary>
        /// <returns></returns>
        public bool IsOccupied()
        {
            return Piece != null;
        }

        /// <summary>
        /// Returns whether this Space is under attack by one of the opponent's pieces
        /// </summary>
        /// <returns></returns>
        public bool IsUnderAttackByOpponent()
        {
            switch (ChessBoard.Turn)
            {
                case Player.Black:
                    return IsUnderAttackByWhite;
                default:
                    return IsUnderAttackByBlack;
            }
        }
    }
}
