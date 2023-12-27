using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public class BishopMovement : IChessMovement
{
    
    private readonly ChessBoard _chessBoard;
    private readonly ChessPiece _bishopPiece;
    private Square CurrentSquare => _bishopPiece.CurrentSquare;
    private Point Pos => _bishopPiece.Pos;

    public BishopMovement(ChessBoard chessBoard, ChessPiece bishopPiece)
    {
        _bishopPiece = bishopPiece;
        _chessBoard = chessBoard;
    }
    public List<Square> GetDefaultSquares(bool checkInspect)
    {
        var squares = new List<Square>();
        squares.AddRange( 
            CommonMovements.PathCalculator(Pos, new Point( -1, -1), _chessBoard, CurrentSquare.SquareState, checkInspect));
        squares.AddRange( 
            CommonMovements.PathCalculator(Pos, new Point( 1, 1), _chessBoard, CurrentSquare.SquareState, checkInspect ));
        squares.AddRange( 
            CommonMovements.PathCalculator(Pos, new Point( 1, -1), _chessBoard, CurrentSquare.SquareState, checkInspect ));
        squares.AddRange( 
            CommonMovements.PathCalculator(Pos, new Point( -1, 1), _chessBoard, CurrentSquare.SquareState, checkInspect ));


        return squares;
    }
}