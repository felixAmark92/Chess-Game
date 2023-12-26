using MainProject.ChessMovements;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.Behaviours.ChessPieces;

public class PawnPiece : ChessPiece
{
    private bool _hasMoved;

    public bool IsMoved()
    {
        return _hasMoved;
    }

    public void SetHasMoved()
    {
        _hasMoved = true;
    }
    public PawnPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new PawnMovement(chessBoard, this);
    }
}