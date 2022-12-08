namespace ConsoleChessV3.Interfaces
{
    internal interface IChessMove
    {
        public void Perform();

        public void Reverse();

        public bool IsValidChessMove();

        
    }
}
