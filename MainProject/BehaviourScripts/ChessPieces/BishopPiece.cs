using MainProject.ChessMovements;
using MainProject.ChessMovements.ChessPins;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts.ChessPieces;

public class BishopPiece : ChessPiece
{
    public BishopPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new BishopMovement(chessBoard, this);
        _pinCalculator = new BishopPinCalculator(chessBoard, this);
    }
}