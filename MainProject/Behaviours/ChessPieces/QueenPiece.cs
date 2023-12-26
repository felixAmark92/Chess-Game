using MainProject.ChessMovements;
using MainProject.ChessMovements.ChessPins;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.Behaviours.ChessPieces;

public class QueenPiece : ChessPiece
{
    public QueenPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new QueenMovement(chessBoard, this);
        _pinCalculator = new QueenPinCalculator(chessBoard, this);
    }
}