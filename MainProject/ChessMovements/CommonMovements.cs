using System;
using System.Collections.Generic;
using MainProject.Behaviours;
using MainProject.Behaviours.ChessPieces;
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
        SquareState squareState
        )
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
    
    public static bool StraightLinePin(
        Point pos, 
        Func<Point> incrementation, 
        ChessBoard chessBoard, 
        SquareState squareState, 
        out ChessPin chessPin
    )
    {
        var squares = new List<Square>();
        squares.Add(chessBoard.Squares[pos.Y, pos.X]);
        ChessPiece? pinnedPiece = null;
        chessPin = new ChessPin();

        pos += incrementation();
        while (chessBoard.InsideChessBoard(pos))
        {
            if (chessBoard.Squares[pos.Y, pos.X].SquareState == SquareState.NotOccupied)
            {
                squares.Add(chessBoard.Squares[pos.Y, pos.X]);
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
                chessPin.PinnedPiece = pinnedPiece;
                chessPin.ViableSquares = squares;

                return true;
            }
            else
            {
                return false;
            }

            pos += incrementation();
        }

        return false;
    }
    
    
    
}