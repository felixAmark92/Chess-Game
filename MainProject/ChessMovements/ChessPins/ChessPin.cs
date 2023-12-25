using System.Collections.Generic;
using MainProject.Behaviours;
using MainProject.Behaviours.ChessPieces;

namespace MainProject.ChessMovements.ChessPins;

public class ChessPin
{
    public ChessPiece PinnedPiece { get; set; }
    
    public List<Square> ViableSquares { get; set; }
    
}