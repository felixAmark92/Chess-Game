using System;
using System.Collections.Generic;
using System.Drawing;
using MainProject.Components;
using MainProject.Entities;

namespace MainProject.ChessMovements;

public class RookMovement : IChessMovement
{
    private readonly ChessBoard _chessBoard;
    private readonly ChessPosition _chessPosition;
    private readonly ChessColor _chessColor;
    private Point _pos;
    private Square CurrentSquare => _chessBoard.Squares[_pos.Y, _pos.X];

    public RookMovement(ChessBoard chessBoard, ChessPiece chessPiece)
    {
        _chessBoard = chessBoard;
        _chessPosition = chessPiece.ChessPosition;
        _chessColor = chessPiece.ChessColor;
    }

    public List<Square> GetMovableSquares()
    {
        var squares = new List<Square>();
        
        Test(()=> _pos.X++);
        Test(()=> _pos.X--);
        Test(()=> _pos.Y++);
        Test(()=> _pos.Y--);

        return squares;

        void Test(Action incrementation)
        {
            _pos = _chessPosition.Position;
            incrementation();
            while (_chessBoard.InsideChessBoard(_pos))
            {
                if (CurrentSquare.SquareState == SquareState.NotOccupied)
                {
                    squares.Add(CurrentSquare);
                }
                else if (CurrentSquare.SquareState == SquareState.OccupiedByBlack)
                {
                    if (_chessColor == ChessColor.Black)
                    {
                        break;
                    }

                    squares.Add(CurrentSquare);
                    break;
                }
                else if (CurrentSquare.SquareState == SquareState.OccupiedByWhite)
                {
                    if (_chessColor == ChessColor.White)
                    {
                        break;
                    }
                    squares.Add(CurrentSquare);
                    break;
                
                }

                incrementation();
            }
        }
    }
}