using System.Collections.Generic;
using MainProject.Behaviours;
using MainProject.Behaviours.ChessPieces;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public class BishopMovement : IChessMovement
{
    
    private readonly ChessBoard _chessBoard;
    private readonly BishopPiece _bishopPiece;
    private readonly Square _startingSquare;
    private Point Pos => _bishopPiece.ChessPosition.Position;

    public BishopMovement(ChessBoard chessBoard, BishopPiece bishopPiece)
    {
        _bishopPiece = bishopPiece;
        _chessBoard = chessBoard;
        _startingSquare = bishopPiece.CurrentSquare;
    }
    public List<Square> GetMovableSquares()
    {
        var squares = new List<Square>();
        CommonMovements.StraightLines(Pos, () => new Point( -1, -1), _chessBoard, squares, _startingSquare.SquareState );
        CommonMovements.StraightLines(Pos, () => new Point( 1, 1), _chessBoard, squares, _startingSquare.SquareState );
        CommonMovements.StraightLines(Pos, () => new Point( -1, 1), _chessBoard, squares, _startingSquare.SquareState );
        CommonMovements.StraightLines(Pos, () => new Point( 1, -1), _chessBoard, squares, _startingSquare.SquareState );

        return squares;
    }
}