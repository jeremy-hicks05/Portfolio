using ConsoleChessV3.Interfaces;
using ConsoleChessV3.Pieces.Black;
using ConsoleChessV3.SuperClasses;

namespace ConsoleChessV3
{
    internal class Space
    {
        public int Column;
        public int Row;
        public IPiece? Piece;

        public Space()
        {
            Column = -1;
            Row = -1;
            Piece = null;
        }

        public Space(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public Space(int column, int row, IPiece piece)
        {
            Column = column;
            Row = row;
            Piece = piece;
        }

        public override string ToString()
        {
            return (Piece == null ? $"[ ]" : $"[{Piece.GetName()}]");
        }

        public void Clear()
        {
            this.Piece = null;
        }

        public bool IsEmpty()
        {
            return Piece == null;
        }

        public bool IsOccupied()
        {
            return Piece != null;
        }
    }
}
