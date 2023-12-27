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
    private readonly ChessBoard _chessBoard;
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
        }
    }

    protected IChessMovement ChessMovement { get; set; }
    protected IPinCalculator _pinCalculator { get; set; } = new NoPinPieceCalculator();
    public ChessColor ChessColor { get; }

    public bool IsPinned;
    public List<Square> ValidPinSquares;

    public Point Pos => CurrentSquare.ChessPosition.Position;

    public ChessPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos)
    {
        ChessColor = chessColor;
        CurrentSquare = chessBoard.Squares[pos.Y, pos.X];
        _chessBoard = chessBoard;
    }

    public override void ComponentsInit()
    {
        Entity.GetComponent<Transform>().Position =
            new Vector2(_currentSquare.ChessPosition.Position.X * _chessBoard.SquaresSize,
                _currentSquare.ChessPosition.Position.Y * _chessBoard.SquaresSize);

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

        if (CheckMateCalculator.KingIsChecked && this is not KingPiece)
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

    public virtual void RemoveSquaresThatAreThreats(List<Square> kingSquares)
    {
        var thisSquares = GetMovableSquares(false);

        var squares = kingSquares.Where(s => thisSquares.Contains(s)).ToList();

        foreach (var square in squares)
        {
            kingSquares.Remove(square);
        }
    }

    public void CalculatePin()
    {
        _pinCalculator.CalculatePin();
    }
}