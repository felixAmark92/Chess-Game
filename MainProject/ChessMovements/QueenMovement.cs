using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public class QueenMovement : IChessMovement
{
    private readonly ChessBoard _chessBoard;
    private readonly ChessPiece _queenPiece;
    private  Square CurrentSquare => _queenPiece.CurrentSquare;
    private Point Pos => _queenPiece.Pos;
    private BishopMovement _bishopMovement;
    private RookMovement _rookMovement;

    public QueenMovement(ChessBoard chessBoard, ChessPiece queenPiece)
    {
        _queenPiece = queenPiece;
        _chessBoard = chessBoard;
        _bishopMovement = new BishopMovement(chessBoard, queenPiece);
        _rookMovement = new RookMovement(chessBoard, queenPiece);
    }

    
    public List<Square> GetDefaultSquares()
    {
        var list = _bishopMovement.GetDefaultSquares();
        var list2 = _rookMovement.GetDefaultSquares();
        
        list.AddRange(list2);

        return list;
    }
}