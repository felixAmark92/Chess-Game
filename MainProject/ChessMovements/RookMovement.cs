using System;
using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
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

    public List<Square> GetDefaultSquares(bool checkInspect)
    {
        var movableSquares = new List<Square>();

        movableSquares.AddRange(
        CommonMovements.pathCalculator(
            Pos, new Point(1, 0), _chessBoard, CurrentSquare.SquareState, checkInspect));
        movableSquares.AddRange(
            CommonMovements.pathCalculator(
                Pos, new Point(-1, 0), _chessBoard, CurrentSquare.SquareState, checkInspect));
        movableSquares.AddRange(
            CommonMovements.pathCalculator(
                Pos, new Point(0, 1), _chessBoard, CurrentSquare.SquareState, checkInspect));
        movableSquares.AddRange(
            CommonMovements.pathCalculator(
                Pos, new Point(0, -1), _chessBoard, CurrentSquare.SquareState, checkInspect));

        return movableSquares;
    }
}