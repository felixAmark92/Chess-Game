using System;
using System.Collections.Generic;
using MainProject.Behaviours;
using MainProject.Behaviours.ChessPieces;
using MainProject.Components;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public class RookMovement : IChessMovement
{
    private readonly ChessBoard _chessBoard;
    private readonly ChessPiece _rookPiece;
    private  Square CurrentSquare => _rookPiece.CurrentSquare;
    private Point Pos => _rookPiece.Pos;

    public RookMovement(ChessBoard chessBoard, ChessPiece rookPiece)
    {
        _rookPiece = rookPiece;
        _chessBoard = chessBoard;
    }

    public List<Square> GetMovableSquares()
    {
        var movableSquares = new List<Square>();

        CommonMovements.StraightLineMovement(
            Pos, () => new Point(1, 0), _chessBoard, movableSquares, CurrentSquare.SquareState);
        CommonMovements.StraightLineMovement(
            Pos, () => new Point(-1, 0), _chessBoard, movableSquares, CurrentSquare.SquareState);
        CommonMovements.StraightLineMovement(
            Pos, () => new Point(0, -1), _chessBoard, movableSquares, CurrentSquare.SquareState);
        CommonMovements.StraightLineMovement(
            Pos, () => new Point(0, 1), _chessBoard, movableSquares, CurrentSquare.SquareState);

        return movableSquares;
    }
}