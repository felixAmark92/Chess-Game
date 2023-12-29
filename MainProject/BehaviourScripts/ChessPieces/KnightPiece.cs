using MainProject.ChessMovements;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts.ChessPieces;

public class KnightPiece : ChessPiece
{
    public KnightPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new KnightMovement(chessBoard, this);
    }

    public override char Get_FEN_Notation()
    {
        return ChessColor == ChessColor.Black ? 'n' : 'N';
    }
}