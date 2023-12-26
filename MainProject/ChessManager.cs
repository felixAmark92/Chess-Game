using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MainProject.BehaviourScripts;
using MainProject.BehaviourScripts.ChessPieces;
using MainProject.ChessMovements.ChessPins;
using MainProject.Components;
using MainProject.EntityLogic;
using MainProject.Enums;
using MainProject.Factories;
using Microsoft.Xna.Framework;

namespace MainProject;

public static class ChessManager
{
    public static ChessPiece? SelectedPiece
    {
        get => _selectedPiece;
        set
        {
            _selectedPiece = value;

            foreach (var square in SelectedPieceMovableSquares)
            {
                square.Entity.GetComponent<Renderer>().Color = Color.White;
            }

            if (_selectedPiece == null)
            {
                SelectedPieceMovableSquares = new List<Square>();
                return;
            }
            SelectedPieceMovableSquares = _selectedPiece.GetMovableSquares();
            foreach (var square in SelectedPieceMovableSquares)
            {
                square.Entity.GetComponent<Renderer>().Color = Color.Green;
            }
            
        }
    }

    public static ChessBoard ChessBoard { get; } = new ChessBoard();
    private static List<ChessPiece> BlackPieces { get; set; }
    private static List<ChessPiece> WhitePieces { get; set; }

    private static ChessColor _playerTurn = ChessColor.White;
    private static ChessPiece? _selectedPiece;

    public static IEnumerable<Square> SelectedPieceMovableSquares { get; private set; } = new List<Square>();
    


    static ChessManager()
    {
        ChessBoard.LoadBoard();
    }

    public static void LoadPieces()
    {
        var BlackPiecesEntities = new List<EntityLogic.Entity>()
        {
            ChessPieceFactory.CreateChessPiece(ChessType.Rook, ChessColor.Black, new Point(7, 0)),
            ChessPieceFactory.CreateChessPiece(ChessType.Rook, ChessColor.Black, new Point(0, 0)),
            ChessPieceFactory.CreateChessPiece(ChessType.Knight, ChessColor.Black, new Point(1, 0)),
            ChessPieceFactory.CreateChessPiece(ChessType.Knight, ChessColor.Black, new Point(6, 0)),
            ChessPieceFactory.CreateChessPiece(ChessType.Bishop, ChessColor.Black, new Point(2, 0)),
            ChessPieceFactory.CreateChessPiece(ChessType.Bishop, ChessColor.Black, new Point(5, 0)),
            ChessPieceFactory.CreateChessPiece(ChessType.Queen, ChessColor.Black, new Point(3, 0)),
            ChessPieceFactory.CreateChessPiece(ChessType.King, ChessColor.Black, new Point(4, 0)),
        };

        for (int i = 0; i < 8; i++)
        {
            var piece = ChessPieceFactory.CreateChessPiece(ChessType.Pawn, ChessColor.Black, new Point(i, 1));
            BlackPiecesEntities.Add(piece);
        }
        
        

        var WhitePiecesEntities = new List<EntityLogic.Entity>()
        {
            ChessPieceFactory.CreateChessPiece(ChessType.Rook, ChessColor.White, new Point(7, 7)),
            ChessPieceFactory.CreateChessPiece(ChessType.Rook, ChessColor.White, new Point(0, 7)),
            ChessPieceFactory.CreateChessPiece(ChessType.Knight, ChessColor.White, new Point(6, 7)),
            ChessPieceFactory.CreateChessPiece(ChessType.Knight, ChessColor.White, new Point(1, 7)),
            ChessPieceFactory.CreateChessPiece(ChessType.Bishop, ChessColor.White, new Point(5, 7)),
            ChessPieceFactory.CreateChessPiece(ChessType.Bishop, ChessColor.White, new Point(2, 7)),
            ChessPieceFactory.CreateChessPiece(ChessType.Queen, ChessColor.White, new Point(3, 7)),
            ChessPieceFactory.CreateChessPiece(ChessType.King, ChessColor.White, new Point(4, 7)),
        };
        
        for (int i = 0; i < 8; i++)
        {
            var piece = ChessPieceFactory.CreateChessPiece(ChessType.Pawn, ChessColor.White, new Point(i, 6));
            WhitePiecesEntities.Add(piece);
        }

        foreach (var entity in BlackPiecesEntities)
        {
            entity.GetComponent<Interactive>().SetInactive();
        }

        WhitePieces = WhitePiecesEntities.Select(e => e.GetBehaviour<ChessPiece>()).ToList();
        BlackPieces = BlackPiecesEntities.Select(e => e.GetBehaviour<ChessPiece>()).ToList();
    }
    
    
    public static void MoveSelectedPiece(Square square)
    {
        foreach (var movableSquare in SelectedPieceMovableSquares)
        {
            movableSquare.Entity.GetComponent<Renderer>().Color = Color.White;
        }
        
        if (square.SquareState != SquareState.NotOccupied)
        {
            if (square.SquareState == SquareState.OccupiedByBlack)
            {
                BlackPieces.Remove(square.OccupyingChessPiece);
                square.OccupyingChessPiece.Entity.Destroy();
            }
            else
            {
                WhitePieces.Remove(square.OccupyingChessPiece);
                square.OccupyingChessPiece.Entity.Destroy();
            }
        }

        if (SelectedPiece is PawnPiece pawnPiece)
        {
            pawnPiece.SetHasMoved();
        }
        SelectedPiece.CurrentSquare = square;
        SelectedPiece.Entity.GetComponent<Transform>().Position = new Vector2(
            square.ChessPosition.Position.X * ChessBoard.SquaresSize,
            square.ChessPosition.Position.Y * ChessBoard.SquaresSize);
        SelectedPiece = null;

        EndTurn();
    }

    private static void EndTurn()
    {
        if (_playerTurn == ChessColor.Black)
        {
            CheckCalculator.CalculateChecks(BlackPieces);
            SwitchActivePieces(BlackPieces, WhitePieces);
            CalculatePins(WhitePieces, BlackPieces);
        }
        else
        {
            CheckCalculator.CalculateChecks(WhitePieces);
            SwitchActivePieces(WhitePieces, BlackPieces);
            CalculatePins(BlackPieces, WhitePieces);
        }
        _playerTurn = _playerTurn == ChessColor.Black ? ChessColor.White : ChessColor.Black;

    }

    private static void SwitchActivePieces(List<ChessPiece> deactivate, List<ChessPiece> activate)
    {
        foreach (var chessPiece in deactivate)
        {
            chessPiece.Entity.GetComponent<Interactive>().SetInactive();
        }
        foreach (var chessPiece in activate)
        {
            chessPiece.Entity.GetComponent<Interactive>().SetActive();
        }
    }
    public static void CalculatePins(List<ChessPiece> attacker, List<ChessPiece> defender)
    {
        foreach (var piece in attacker)
        {
            piece.IsPinned = false;
        }

        foreach (var piece in defender)
        {
            piece.CalculatePin();
        }
    }
}