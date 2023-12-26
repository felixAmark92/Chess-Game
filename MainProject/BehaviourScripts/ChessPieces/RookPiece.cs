using MainProject.ChessMovements;
using MainProject.ChessMovements.ChessPins;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts.ChessPieces;

public class RookPiece : ChessPiece
{
    public RookPiece(ChessColor color, ChessBoard chessBoard, Point pos) 
        : base(color, chessBoard, pos)
    {
        ChessMovement = new RookMovement(chessBoard, this);
        _pinCalculator = new RookPinCalculator(chessBoard, this);
    }
    
}