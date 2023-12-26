using MainProject.ChessMovements;
using MainProject.ChessMovements.ChessPins;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts.ChessPieces;

public class KingPiece : ChessPiece
{
    public KingPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new KingMovement(chessBoard, this);
        _pinCalculator = new NoPinPieceCalculator();
    }
}