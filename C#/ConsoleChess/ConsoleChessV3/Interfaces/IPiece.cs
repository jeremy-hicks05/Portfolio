﻿using ConsoleChessV3.Enums;

namespace ConsoleChessV3.Interfaces
{
    internal interface IPiece
    {
        // legal attempt at moving - used for pre-move options
        public bool CanLegallyTryToMoveFromSpaceToSpace();

        // legal attempt at capturing - used for pre-move options
        public bool CanLegallyTryToCaptureFromSpaceToSpace();

        public void BuildListOfSpacesToInspect();

        // checking if piece is blocked by anything but open spaces
        // before it gets to its destination
        public bool IsBlocked();

        // attempt move, then test for check status on player's king
        public bool TryMove();

        // attempt capture, then test for check status on player's king
        public bool TryCapture();

        // return affected pieces for successful move
        // to be used in takeback function
        public IChessMove Move();

        // return affected pieces for successful capture
        // to be used in takeback function
        public IChessMove Capture();

        // getters and setters
        public string GetName();
        public int GetPointValue();
        public bool GetHasMoved();
        public Player GetBelongsTo();
    }
}
