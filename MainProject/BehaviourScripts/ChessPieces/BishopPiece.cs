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
        PinCalculator = new BishopPinCalculator(chessBoard, this);
    }

    public override char Get_FEN_Notation()
    {
        return ChessColor == ChessColor.Black ? 'b' : 'B';
    }
}