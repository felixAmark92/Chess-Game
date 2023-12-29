using MainProject.ChessMovements;
using MainProject.ChessMovements.ChessPins;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts.ChessPieces;

public class RookPiece : ChessPiece
{
    public bool HasMoved { get; set; }
    public RookPiece(ChessColor color, ChessBoard chessBoard, Point pos) 
        : base(color, chessBoard, pos)
    {
        ChessMovement = new RookMovement(chessBoard, this);
        PinCalculator = new RookPinCalculator(chessBoard, this);
    }
    public override char Get_FEN_Notation()
    {
        return ChessColor == ChessColor.Black ? 'r' : 'R';
    }
}