using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public class KingMovement : IChessMovement
{
    private readonly ChessBoard _chessBoard;
    private readonly KingPiece _kingPiece;
    private Square CurrentSquare => _kingPiece.CurrentSquare;
    private Point Pos => _kingPiece.Pos;

    public KingMovement(ChessBoard chessBoard, KingPiece kingPiece)
    {
        _kingPiece = kingPiece;
        _chessBoard = chessBoard;
    }

    public List<Square> GetDefaultSquares(bool checkInspect)
    {
        var movableSquares = new List<Square>();
        if (_kingPiece.HasMoved == false)
        {
            if ( _chessBoard.Squares[Pos.Y, Pos.X + 1].SquareState == SquareState.NotOccupied && 
                 _chessBoard.Squares[Pos.Y, Pos.X + 2].SquareState == SquareState.NotOccupied)
            {
                if (_chessBoard.Squares[Pos.Y, Pos.X + 3].OccupyingChessPiece is RookPiece { HasMoved: false })
                {
                    movableSquares.Add(_chessBoard.Squares[Pos.Y, Pos.X + 2]);
                }
            }
        }

        for (int y = Pos.Y - 1; y < Pos.Y + 2; y++)
        {
            for (int x = Pos.X - 1; x < Pos.X + 2; x++)
            {
                if (!_chessBoard.InsideChessBoard(new Point(x, y)))
                {
                    continue;
                }

                if (_chessBoard.Squares[y, x].SquareState == CurrentSquare.SquareState)
                {
                    continue;
                }

                movableSquares.Add(_chessBoard.Squares[y, x]);
            }
        }

        return movableSquares;
    }

}