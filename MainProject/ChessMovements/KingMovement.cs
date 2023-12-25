using System.Collections.Generic;
using MainProject.Behaviours;
using MainProject.Behaviours.ChessPieces;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public class KingMovement : IChessMovement
{
    private readonly ChessBoard _chessBoard;
    private readonly ChessPiece _kingPiece;
    private Square CurrentSquare => _kingPiece.CurrentSquare;
    private Point Pos => _kingPiece.Pos;

    public KingMovement(ChessBoard chessBoard, ChessPiece kingPiece)
    {
        _kingPiece = kingPiece;
        _chessBoard = chessBoard;
    }

    public List<Square> GetMovableSquares()
    {
        var list = new List<Square>();

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

                list.Add(_chessBoard.Squares[y, x]);
            }
        }

        return list;
    }

}