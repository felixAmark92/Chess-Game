using System;
using System.Collections.Generic;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.ChessMovements;

public class PawnMovement : IChessMovement
{
    private readonly ChessBoard _chessBoard;
    private readonly PawnPiece _pawnPiece;
    private Square CurrentSquare => _pawnPiece.CurrentSquare;
    private Point Pos => _pawnPiece.Pos;
    private bool HaveMoved => _pawnPiece.IsMoved();
    
    public PawnMovement(ChessBoard chessBoard, PawnPiece pawnPiece)
    {
        _pawnPiece = pawnPiece;
        _chessBoard = chessBoard;
    }
    
    public List<Square> GetDefaultSquares(bool checkInspect)
    {
        var squares = new List<Square>();
        Point direction = _pawnPiece.ChessColor == ChessColor.Black 
            ? new Point(0, 1) : new Point(0, -1);
        if (!HaveMoved)
        {
            squares.AddRange(
            CommonMovements.PathCalculator(
                Pos, direction, 
                _chessBoard, 
                CurrentSquare.SquareState,
                2,
                false,
                checkInspect));
        }
        else
        {
            squares.AddRange(
            CommonMovements.PathCalculator(
                Pos, direction, 
                _chessBoard, 
                CurrentSquare.
                    SquareState, 
                1, 
                false,
                checkInspect));
        }

        var rightSide = Pos + direction + new Point(1, 0);
        if (_chessBoard.GetIfInsideChessBoard(rightSide))
        {
            if (ChessManager.BlackGhostPawn is not null)
            {
                if (_pawnPiece.ChessColor == ChessColor.White &&
                    ChessManager.BlackGhostPawn.Square == _chessBoard.Squares[rightSide.Y, rightSide.X])
                {
                    squares.Add(_chessBoard.Squares[rightSide.Y, rightSide.X]);   
                }
                
            }
            if (ChessManager.WhiteGhostPawn is not null)
            {
                if (_pawnPiece.ChessColor == ChessColor.Black &&
                    ChessManager.WhiteGhostPawn.Square == _chessBoard.Squares[rightSide.Y, rightSide.X])
                {
                    squares.Add(_chessBoard.Squares[rightSide.Y, rightSide.X]);   
                }
                
            }
            if (_chessBoard.Squares[rightSide.Y, rightSide.X].SquareState != CurrentSquare.SquareState && 
                _chessBoard.Squares[rightSide.Y, rightSide.X].SquareState != SquareState.NotOccupied)
            {
                squares.Add(_chessBoard.Squares[rightSide.Y, rightSide.X]);
                if (_chessBoard.Squares[rightSide.Y, rightSide.X].OccupyingChessPiece is KingPiece && checkInspect)
                {
                    CheckMateCalculator.SetKingIsChecked(_chessBoard.Squares[rightSide.Y, rightSide.X].OccupyingChessPiece.ChessColor);
                    CheckMateCalculator.AttackerPaths.Add(new List<Square>(){ _chessBoard.Squares[rightSide.Y, rightSide.X] } );
                    CheckMateCalculator.AttackingPieceSquare.Add(CurrentSquare);
                }
                
            }

            if (_chessBoard.Squares[rightSide.Y, rightSide.X].SquareState == CurrentSquare.SquareState)
            {
                _chessBoard.Squares[rightSide.Y, rightSide.X].OccupyingChessPiece.IsGuarded = true;
                if (_chessBoard.Squares[rightSide.Y, rightSide.X].OccupyingChessPiece is QueenPiece)
                {
                    Console.WriteLine("pawn is guarding queenpeice");
                }
            }
        }
        
        var leftSide = Pos + direction + new Point(-1, 0);
        if (_chessBoard.GetIfInsideChessBoard(leftSide))
        {
            if (ChessManager.BlackGhostPawn is not null)
            {
                if (_pawnPiece.ChessColor == ChessColor.White &&
                    ChessManager.BlackGhostPawn.Square == _chessBoard.Squares[leftSide.Y, leftSide.X])
                {
                    squares.Add(_chessBoard.Squares[leftSide.Y, leftSide.X]);   
                }
            }
            if (ChessManager.WhiteGhostPawn is not null)
            {
                if (_pawnPiece.ChessColor == ChessColor.Black &&
                    ChessManager.WhiteGhostPawn.Square == _chessBoard.Squares[leftSide.Y, leftSide.X])
                {
                    squares.Add(_chessBoard.Squares[leftSide.Y, leftSide.X]);   
                }
                
            }
            if (_chessBoard.Squares[leftSide.Y, leftSide.X].SquareState != CurrentSquare.SquareState && 
                _chessBoard.Squares[leftSide.Y, leftSide.X].SquareState != SquareState.NotOccupied)
            {
                squares.Add(_chessBoard.Squares[leftSide.Y, leftSide.X]);
                if (_chessBoard.Squares[leftSide.Y, leftSide.X].OccupyingChessPiece is KingPiece && checkInspect)
                {
                    CheckMateCalculator.SetKingIsChecked(_chessBoard.Squares[leftSide.Y, leftSide.X].OccupyingChessPiece.ChessColor);
                    CheckMateCalculator.AttackerPaths.Add(new List<Square>(){ _chessBoard.Squares[leftSide.Y, leftSide.X] } );
                    CheckMateCalculator.AttackingPieceSquare.Add(CurrentSquare);
                }
            }
            if (_chessBoard.Squares[leftSide.Y, leftSide.X].SquareState == CurrentSquare.SquareState)
            {
                _chessBoard.Squares[leftSide.Y, leftSide.X].OccupyingChessPiece.IsGuarded = true;
                if (_chessBoard.Squares[leftSide.Y, leftSide.X].OccupyingChessPiece is QueenPiece)
                {
                    Console.WriteLine("pawn is guarding queenpeice");
                }
            }
        }

        return squares;
    }
}