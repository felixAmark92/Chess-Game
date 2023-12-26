using MainProject.ChessMovements;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.Behaviours.ChessPieces;

public class KnightPiece : ChessPiece
{
    public KnightPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new KnightMovement(chessBoard, this);
    }
}