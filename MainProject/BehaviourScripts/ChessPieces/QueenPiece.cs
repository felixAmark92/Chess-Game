using MainProject.ChessMovements;
using MainProject.ChessMovements.ChessPins;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts.ChessPieces;

public class QueenPiece : ChessPiece
{
    public QueenPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new QueenMovement(chessBoard, this);
        PinCalculator = new QueenPinCalculator(chessBoard, this);
    }

    public override char Get_FEN_Notation()
    {
        return ChessColor == ChessColor.Black ? 'q' : 'Q';
    }
}