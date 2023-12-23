using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public static class CommonMovements
{
    public static void StraightLines(
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
    
}