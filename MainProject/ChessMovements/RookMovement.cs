using System;
using System.Collections.Generic;
using MainProject.Components;
using MainProject.Entities;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public class RookMovement : IChessMovement
{
    private readonly ChessBoard _chessBoard;
    private readonly ChessPiece _rookPiece;
    private readonly Square _startingSquare;
    private Point Pos => _rookPiece.ChessPosition.Position;

    public RookMovement(ChessBoard chessBoard, ChessPiece rookPiece)
    {
        _rookPiece = rookPiece;
        _chessBoard = chessBoard;
        _startingSquare = rookPiece.CurrentSquare;
    }

    public List<Square> GetMovableSquares()
    {
        var movableSquares = new List<Square>();

        CommonMovements.StraightLines(
            Pos, () => new Point(1, 0), _chessBoard, movableSquares, _startingSquare.SquareState);
        CommonMovements.StraightLines(
            Pos, () => new Point(-1, 0), _chessBoard, movableSquares, _startingSquare.SquareState);
        CommonMovements.StraightLines(
            Pos, () => new Point(0, -1), _chessBoard, movableSquares, _startingSquare.SquareState);
        CommonMovements.StraightLines(
            Pos, () => new Point(0, 1), _chessBoard, movableSquares, _startingSquare.SquareState);

        return movableSquares;
    }
}