﻿using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public class KnightMovement : IChessMovement
{
    
    private readonly ChessBoard _chessBoard;
    private readonly ChessPiece _knightPiece;
    private  Square CurrentSquare => _knightPiece.CurrentSquare;
    private Point Pos => _knightPiece.Pos;

    public KnightMovement(ChessBoard chessBoard, ChessPiece knightPiece)
    {
        _knightPiece = knightPiece;
        _chessBoard = chessBoard;
    }

    public List<Square> GetDefaultSquares()
    {
        var squares = new List<Square>();
        
        squares.AddRange(CommonMovements.pathCalculator(
            Pos, new Point(1, 2), _chessBoard ,  CurrentSquare.SquareState, 1, true ));
        squares.AddRange(CommonMovements.pathCalculator(
            Pos, new Point(-1, 2), _chessBoard ,  CurrentSquare.SquareState, 1, true ));
        squares.AddRange(CommonMovements.pathCalculator(
            Pos, new Point(-1, -2), _chessBoard ,  CurrentSquare.SquareState, 1, true ));
        squares.AddRange(CommonMovements.pathCalculator(
            Pos, new Point(1, -2), _chessBoard ,  CurrentSquare.SquareState, 1, true ));
        squares.AddRange(CommonMovements.pathCalculator(
            Pos, new Point(2, 1), _chessBoard ,  CurrentSquare.SquareState, 1, true ));
        squares.AddRange(CommonMovements.pathCalculator(
            Pos, new Point(2, -1), _chessBoard ,  CurrentSquare.SquareState, 1, true ));
        squares.AddRange(CommonMovements.pathCalculator(
            Pos, new Point(-2, 1), _chessBoard ,  CurrentSquare.SquareState, 1, true ));
        squares.AddRange(CommonMovements.pathCalculator(
            Pos, new Point(-2, -1), _chessBoard ,  CurrentSquare.SquareState, 1, true ));
        
        return squares;
    }
}