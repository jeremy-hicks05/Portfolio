﻿namespace ConsoleChessV2.Pieces
{
    internal class WhitePawn : Piece
    {
        public WhitePawn()
        {
            Name = "[P]";
            PointValue = 1;
            BelongsTo = Player.White;
        }

        public override bool CanLegallyTryToMoveFromSpaceToSpace(Space fromSpace, Space toSpace)
        {
            if (fromSpace == toSpace)
            {
                return false;
            }
            if (
                fromSpace.Column == toSpace.Column && fromSpace.Row + 1  == toSpace.Row ||
                //(hasMoved == false && 
                fromSpace.Column == toSpace.Column && fromSpace.Row + 2 == toSpace.Row || // add ) before or
                (fromSpace.Column - 1 == toSpace.Column && fromSpace.Row + 1 == toSpace.Row) ||
                (fromSpace.Column + 1 == toSpace.Column && fromSpace.Row + 1 == toSpace.Row))
            {
                return true;
            }
            return false;
        }
        public override void CreateListOfPiecesToInspect(Space fromSpace, Space toSpace)
        {
            spacesThisPieceCanMoveTo?.Clear();
            if (toSpace.Column == fromSpace.Column)
            {
                // moving up
                for (int row = fromSpace.Row + 1; row <= toSpace.Row; row++)
                {
                    spacesThisPieceCanMoveTo!.Add(ChessBoard.Spaces![fromSpace.Column][row]);
                }
            }
            else if (fromSpace.Column - 1 == toSpace.Column && fromSpace.Row + 1 == toSpace.Row)
            {
                // attacking up and left
                spacesThisPieceCanMoveTo!.Add(ChessBoard.Spaces![toSpace.Column][toSpace.Row]);
            }
            else if (fromSpace.Column + 1 == toSpace.Column && fromSpace.Row + 1 == toSpace.Row)
            {
                // attacking up and right
                spacesThisPieceCanMoveTo!.Add(ChessBoard.Spaces![toSpace.Column][toSpace.Row]);

            }
        }
    }
}
