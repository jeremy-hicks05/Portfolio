namespace ConsoleChessV2
{
    internal class Space
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public Piece? Piece { get; set; }
        public bool IsUnderAttackByWhite;
        public bool IsUnderAttackByBlack;

        public Space(int column, int row, Piece? piece)
        {
            Column = column;
            Row = row;
            Piece = piece;
        }

        public override string ToString()
        {
            return $"[{Piece?.Name}]";
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Space {Column}, {Row} \n has {Piece} on it");
        }

        public void Clear()
        {
            this.Piece = new Piece();
        }
    }
}
