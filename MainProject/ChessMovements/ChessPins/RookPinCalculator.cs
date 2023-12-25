using System.Collections.Generic;
using MainProject.Behaviours;
using MainProject.Behaviours.ChessPieces;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements.ChessPins;

public class RookPinCalculator : IPinCalculator
{
    private readonly ChessBoard _chessBoard;
    private readonly ChessPiece _rookPiece;
    public ChessPiece PinnedPiece { get; set; }

    private Square CurrentSquare => _rookPiece.CurrentSquare;
    private Point Pos => _rookPiece.Pos;


    public RookPinCalculator(ChessBoard chessBoard, ChessPiece rookPiece)
    {
        _rookPiece = rookPiece;
        _chessBoard = chessBoard;
    }

    public ChessPin CalculatePin()
    {
        if (CommonMovements.StraightLinePin(
                Pos,
                () => new Point(1, 0),
                _chessBoard,
                CurrentSquare.SquareState,
                out ChessPin chessPin1))
        {
            return chessPin1;
        }
        if (CommonMovements.StraightLinePin(
                Pos,
                () => new Point(-1, 0),
                _chessBoard,
                CurrentSquare.SquareState,
                out ChessPin chessPin2))
        {
            return chessPin2;
        }
        if (CommonMovements.StraightLinePin(
                Pos,
                () => new Point(0, 1),
                _chessBoard,
                CurrentSquare.SquareState,
                out ChessPin chessPin3))
        {
            return chessPin3;
        }
        if (CommonMovements.StraightLinePin(
                Pos,
                () => new Point(0, -1),
                _chessBoard,
                CurrentSquare.SquareState,
                out ChessPin chessPin4))
        {
            return chessPin4;
        }

        return null;
    }
}