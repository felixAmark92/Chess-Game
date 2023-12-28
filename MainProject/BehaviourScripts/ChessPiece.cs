using System;
using System.Collections.Generic;
using System.Linq;
using MainProject.BehaviourScripts.ChessPieces;
using MainProject.ChessMovements;
using MainProject.ChessMovements.ChessPins;
using MainProject.Components;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts;

public abstract class ChessPiece : Behaviour
{
    protected readonly ChessBoard ChessBoard;
    private Square _currentSquare;
    
    

    public Square CurrentSquare
    {
        get => _currentSquare;
        set
        {
            
            if (_currentSquare is null)
            {
                _currentSquare = value;
                _currentSquare.OccupyingChessPiece = this;
                return;
            }

            _currentSquare.OccupyingChessPiece = null;
            _currentSquare = value;
            _currentSquare.OccupyingChessPiece = this;
            Entity.GetComponent<Transform>().Position = new Vector2(
                Pos.X * ChessBoard.SquaresSize,
                Pos.Y * ChessBoard.SquaresSize);
        }
    }

    public bool IsGuarded { get; set; }
    protected IChessMovement ChessMovement { get; set; }
    protected IPinCalculator PinCalculator { get; set; } = new NoPinPieceCalculator();
    public ChessColor ChessColor { get; }
    public bool IsPinned { get; set; }
    public List<Square> ValidPinSquares { get; set; }

    public Point Pos => CurrentSquare.ChessPosition.Position;

    public ChessPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos)
    {
        ChessColor = chessColor;
        CurrentSquare = chessBoard.Squares[pos.Y, pos.X];
        ChessBoard = chessBoard;
    }

    public override void ComponentsInit()
    {
        Entity.GetComponent<Transform>().Position =
            new Vector2(_currentSquare.ChessPosition.Position.X * ChessBoard.SquaresSize,
                _currentSquare.ChessPosition.Position.Y * ChessBoard.SquaresSize);

        Entity.GetComponent<Renderer>().LayerDepth = 1f;

        Entity.GetComponent<Interactive>().OnLeftClick += () =>
        {
            Console.WriteLine($"{Entity.Id}: {Entity.GetComponent<Renderer>().LayerDepth}");

            ChessManager.SelectedPiece = this;
        };
    }

    public virtual List<Square> GetMovableSquares(bool checkInspect)
    {
        var movableSquares = ChessMovement.GetDefaultSquares(checkInspect);
        var copy = new List<Square>(movableSquares);

        if (IsPinned)
        {
            foreach (var square in copy)
            {
                if (!ValidPinSquares.Contains(square))
                {
                    movableSquares.Remove(square);
                }
            }
        }

        if (CheckMateCalculator.KingIsChecked && this is not KingPiece && CheckMateCalculator.KingChessColor == ChessColor)
        {
            if (CheckMateCalculator.AttackerSquares.Count > 1)
            {
                return new List<Square>();
            }

            foreach (var square in copy)
            {
                if (!CheckMateCalculator.AttackerPaths[0].Contains(square) && CheckMateCalculator.AttackerSquares[0] != square)
                {
                    movableSquares.Remove(square);
                }
            }
        }

        return movableSquares;
    }

    public virtual void RemoveSquaresThatAreAttacked(KingPiece kingPiece, List<Square> kingSquares)
    {
        var thisSquares = 
            this is KingPiece ? ChessMovement.GetDefaultSquares(false) : GetMovableSquares(false);

        var squares = kingSquares.Where(s => thisSquares.Contains(s)).ToList();

        foreach (var square in squares)
        {
            if (kingPiece.HasMoved == false && square == ChessBoard.Squares[kingPiece.Pos.Y, kingPiece.Pos.X + 1] )
            {
                kingSquares.Remove(ChessBoard.Squares[kingPiece.Pos.Y, kingPiece.Pos.X + 2]);
            }
            if (kingPiece.HasMoved == false && square == ChessBoard.Squares[kingPiece.Pos.Y, kingPiece.Pos.X - 1] )
            {
                kingSquares.Remove(ChessBoard.Squares[kingPiece.Pos.Y, kingPiece.Pos.X - 2]);
            }
            kingSquares.Remove(square);
        }
    }

    public void CalculatePin()
    {
        PinCalculator.CalculatePin();
    }
}