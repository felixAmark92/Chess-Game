﻿using System.Collections.Generic;
using System.Linq;
using MainProject.ChessMovements;
using MainProject.Enums;
using Microsoft.Xna.Framework;
namespace MainProject.BehaviourScripts.ChessPieces;
public class PawnPiece : ChessPiece
{
    private bool _hasMoved;
    
    public bool IsMoved()
    {
        return _hasMoved;
    }
    public void SetHasMoved()
    {
        _hasMoved = true;
    }
    public PawnPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos) : base(chessColor, chessBoard, pos)
    {
        ChessMovement = new PawnMovement(chessBoard, this);
    }
    public override void RemoveSquaresThatAreAttacked(KingPiece kingPiece, List<Square> kingSquares)
    {
        Point direction = ChessColor == ChessColor.Black 
            ? new Point(0, 1) : new Point(0, -1);

        var thisSquares = new List<Square>();
        
        var rightSide = Pos + direction + new Point(1, 0);
        if (ChessManager.ChessBoard.GetIfInsideChessBoard(rightSide))
        {
            if (ChessManager.ChessBoard.Squares[rightSide.Y, rightSide.X].SquareState == SquareState.NotOccupied)
            {
                thisSquares.Add(ChessManager.ChessBoard.Squares[rightSide.Y, rightSide.X]);
            }

            if (ChessManager.ChessBoard.Squares[rightSide.Y, rightSide.X].SquareState == CurrentSquare.SquareState)
            {
                ChessManager.ChessBoard.Squares[rightSide.Y, rightSide.X].OccupyingChessPiece.IsGuarded = true;
            }
        }
        var leftSide = Pos + direction + new Point(-1, 0);
        if (ChessManager.ChessBoard.GetIfInsideChessBoard(leftSide))
        {
            if (ChessManager.ChessBoard.Squares[leftSide.Y, leftSide.X].SquareState == SquareState.NotOccupied)
            {
                thisSquares.Add(ChessManager.ChessBoard.Squares[leftSide.Y, leftSide.X]);
            }
            if (ChessManager.ChessBoard.Squares[leftSide.Y, leftSide.X].SquareState == CurrentSquare.SquareState)
            {
                ChessManager.ChessBoard.Squares[leftSide.Y, leftSide.X].OccupyingChessPiece.IsGuarded = true;
            }
        }
        var squares = kingSquares.Where(s => thisSquares.Contains(s)).ToList();

        foreach (var square in squares)
        {
            if (kingPiece.HasMoved == false && square == ChessBoard.Squares[kingPiece.Pos.Y, kingPiece.Pos.X + 1] )
            {
                kingSquares.Remove(ChessBoard.Squares[kingPiece.Pos.Y, kingPiece.Pos.X + 2]);
            }
            kingSquares.Remove(square);
        }
    }

    public override char Get_FEN_Notation()
    {
        return ChessColor == ChessColor.Black ? 'p' : 'P';
    }
}