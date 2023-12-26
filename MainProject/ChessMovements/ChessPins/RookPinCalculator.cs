using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements.ChessPins;

public class RookPinCalculator : IPinCalculator
{
    private readonly ChessBoard _chessBoard;
    private readonly ChessPiece _rookPiece;

    private Square CurrentSquare => _rookPiece.CurrentSquare;
    private Point Pos => _rookPiece.Pos;


    public RookPinCalculator(ChessBoard chessBoard, ChessPiece rookPiece)
    {
        _rookPiece = rookPiece;
        _chessBoard = chessBoard;
    }

    public void CalculatePin()
    {
        if (CommonMovements.PinCalculator(
                Pos,
                 new Point(1, 0),
                _chessBoard,
                CurrentSquare.SquareState))
        {
            return;
        }
        if (CommonMovements.PinCalculator(
                Pos,
                new Point(-1, 0),
                _chessBoard,
                CurrentSquare.SquareState))
        {
            return;
        }
        if (CommonMovements.PinCalculator(
                Pos,
                new Point(0, 1),
                _chessBoard,
                CurrentSquare.SquareState))
        {
            return;
        }
        if (CommonMovements.PinCalculator(
                Pos,
                new Point(0, -1),
                _chessBoard,
                CurrentSquare.SquareState))
        {
            return;
        }
        
    }
}