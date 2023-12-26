using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements.ChessPins;

public class BishopPinCalculator: IPinCalculator
{
    private readonly ChessBoard _chessBoard;
    private readonly ChessPiece _bishopPiece;

    private Square CurrentSquare => _bishopPiece.CurrentSquare;
    private Point Pos => _bishopPiece.Pos;


    public BishopPinCalculator(ChessBoard chessBoard, ChessPiece bishopPiece)
    {
        _bishopPiece = bishopPiece;
        _chessBoard = chessBoard;
    }

    public void CalculatePin()
    {
        Point[] points =
        {
            new Point(-1, -1), 
            new Point(1, 1), 
            new Point(-1, 1),
            new Point(1, -1)
        };

        foreach (var point in points)
        {
            if (CommonMovements.PinCalculator(Pos, point, _chessBoard, CurrentSquare.SquareState))
            {
                return;
            }
        }


    }
}