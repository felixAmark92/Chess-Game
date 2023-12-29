using System.Collections.Generic;
using MainProject.ChessMovements;
using MainProject.ChessMovements.ChessPins;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts.ChessPieces;

public class KingPiece : ChessPiece
{
    public bool HasMoved { get; set; }
    public KingPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new KingMovement(chessBoard, this);
        PinCalculator = new NoPinPieceCalculator();
    }

    public override List<Square> GetMovableSquares(bool checkInspect)
    {
        var baseList = base.GetMovableSquares(checkInspect);

        CheckMateCalculator.CalculateThreats(this, baseList);

        var copy = new List<Square>(baseList);

        foreach (var square in copy)
        {
            if (square.OccupyingChessPiece is null)
            {
                continue;
            }
            if (square.OccupyingChessPiece.IsGuarded)
            {
                baseList.Remove(square);
            }
        }
        
        return baseList;
    }

    public override char Get_FEN_Notation()
    {
        return ChessColor == ChessColor.Black ? 'k' : 'K';
    }
}