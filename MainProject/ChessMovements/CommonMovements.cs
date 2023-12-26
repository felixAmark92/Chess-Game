using System;
using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using MainProject.ChessMovements.ChessPins;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public static class CommonMovements
{
    public static void StraightLineMovement(
        Point pos, 
        Func<Point> incrementation, 
        ChessBoard chessBoard, 
        List<Square> squares,
        SquareState squareState)
    {

        pos += incrementation();
        while (chessBoard.InsideChessBoard(pos))
        {
            if (chessBoard.Squares[pos.Y, pos.X].SquareState == SquareState.NotOccupied)
            {
                squares.Add(chessBoard.Squares[pos.Y, pos.X]);
            }
            else if (chessBoard.Squares[pos.Y, pos.X].SquareState == squareState)
            {
                break;
            }
            else 
            {
                squares.Add(chessBoard.Squares[pos.Y, pos.X]);
                break;
            }
            pos += incrementation();
        }
    }
    
    public static void StraightLineMovement(
        Point pos, 
        Func<Point> incrementation, 
        ChessBoard chessBoard, 
        List<Square> squares,
        SquareState squareState,
        int limit,
        bool aggressive)
    {
        var i = 0;
        pos += incrementation();
        while (chessBoard.InsideChessBoard(pos) && i < limit)
        {
            i++;
            if (chessBoard.Squares[pos.Y, pos.X].SquareState == SquareState.NotOccupied)
            {
                squares.Add(chessBoard.Squares[pos.Y, pos.X]);
            }
            else
            {
                if (aggressive && chessBoard.Squares[pos.Y, pos.X].SquareState != squareState)
                {
                    squares.Add(chessBoard.Squares[pos.Y, pos.X]);
                }
                break;
            }
            
            pos += incrementation();
        }
    }
    
    public static bool PinCalculator(
        Point pos, 
        Point incrementation, 
        ChessBoard chessBoard, 
        SquareState squareState)
    {
        ChessPiece pinnedPiece = null;
        var chessPin = new List<Square>();
        chessPin.Add(chessBoard.Squares[pos.Y, pos.X]);
        pos += incrementation;
        while (chessBoard.InsideChessBoard(pos))
        {
            if (chessBoard.Squares[pos.Y, pos.X].SquareState == SquareState.NotOccupied)
            {
                chessPin.Add(chessBoard.Squares[pos.Y, pos.X]);
            }
            else if (chessBoard.Squares[pos.Y, pos.X].SquareState == squareState)
            {
                return false;
            }
            else if (pinnedPiece is null)
            {
                pinnedPiece = chessBoard.Squares[pos.Y, pos.X].OccupyingChessPiece;
            }
            else if (chessBoard.Squares[pos.Y, pos.X].OccupyingChessPiece is KingPiece)
            {
                pinnedPiece.IsPinned = true;
                pinnedPiece.ValidPinSquares = chessPin;
                return true;
            }
            else
            {
                return false;
            }
            pos += incrementation;
        }

        return false;
    }
    
    
    
}