using System;
using System.Collections.Generic;
using MainProject.Behaviours;
using MainProject.Behaviours.ChessPieces;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public class PawnMovement : IChessMovement
{
    private readonly ChessBoard _chessBoard;
    private readonly PawnPiece _pawnPiece;
    private Square CurrentSquare => _pawnPiece.CurrentSquare;
    private Point Pos => _pawnPiece.Pos;
    private bool HaveMoved => _pawnPiece.IsMoved();
    
    public PawnMovement(ChessBoard chessBoard, PawnPiece pawnPiece)
    {
        _pawnPiece = pawnPiece;
        _chessBoard = chessBoard;
    }
    
    public List<Square> GetMovableSquares()
    {
        var squares = new List<Square>();
        Point direction = _pawnPiece.ChessColor == ChessColor.Black 
            ? new Point(0, 1) : new Point(0, -1);
        if (!HaveMoved)
        {
            CommonMovements.StraightLineMovement(
                Pos, ()=> direction, 
                _chessBoard, 
                squares, 
                CurrentSquare.SquareState,
                2,
                false);
        }
        else
        {
            CommonMovements.StraightLineMovement(
                Pos, ()=> direction, 
                _chessBoard, 
                squares, 
                CurrentSquare.
                    SquareState, 
                1, 
                false);
        }

        var sideway1 = Pos + direction + new Point(1, 0);
        if (_chessBoard.InsideChessBoard(sideway1))
        {
            if (_chessBoard.Squares[sideway1.Y, sideway1.X].SquareState != CurrentSquare.SquareState && 
                _chessBoard.Squares[sideway1.Y, sideway1.X].SquareState != SquareState.NotOccupied)
            {
                squares.Add(_chessBoard.Squares[sideway1.Y, sideway1.X]);
                
            }
        }
        
        var sideway2 = Pos + direction + new Point(-1, 0);
        if (_chessBoard.InsideChessBoard(sideway2))
        {
            if (_chessBoard.Squares[sideway2.Y, sideway2.X].SquareState != CurrentSquare.SquareState && 
                _chessBoard.Squares[sideway2.Y, sideway2.X].SquareState != SquareState.NotOccupied)
            {
                squares.Add(_chessBoard.Squares[sideway2.Y, sideway2.X]);
                
            }
        }

        return squares;
    }
}