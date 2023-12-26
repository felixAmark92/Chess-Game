using MainProject.Behaviours;
using MainProject.Behaviours.ChessPieces;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements.ChessPins;

public class QueenPinCalculator : IPinCalculator
{
    private readonly ChessBoard _chessBoard;
    private readonly ChessPiece _queenPiece;
    public ChessPiece PinnedPiece { get; set; }

    private Square CurrentSquare => _queenPiece.CurrentSquare;
    private Point Pos => _queenPiece.Pos;
    private RookPinCalculator _rookPinCalculator;
    private BishopPinCalculator _bishopPinCalculator;


    public QueenPinCalculator(ChessBoard chessBoard, ChessPiece queenPiece)
    {
        _queenPiece = queenPiece;
        _chessBoard = chessBoard;
        _bishopPinCalculator = new BishopPinCalculator(chessBoard, queenPiece);
        _rookPinCalculator = new RookPinCalculator(chessBoard, queenPiece);
    }

    public ChessPin CalculatePin()
    {
        var getPin = _bishopPinCalculator.CalculatePin();

        if (getPin is null)
        {
            return _rookPinCalculator.CalculatePin();
        }
        return getPin;
    }
}