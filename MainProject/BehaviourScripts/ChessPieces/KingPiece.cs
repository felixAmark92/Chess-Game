using System.Collections.Generic;
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

    public override List<Square> GetMovableSquares(bool checkInspect)
    {
        var baseList = base.GetMovableSquares(checkInspect);

        CheckMateCalculator.CalculateThreats(this, baseList);
        return baseList;
    }

    public List<Square> GetDefaultMovableSquares(bool checkInspect)
    {
        return ChessMovement.GetDefaultSquares(checkInspect);
    }

}