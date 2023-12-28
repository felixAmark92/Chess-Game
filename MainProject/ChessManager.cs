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
            SelectedPieceMovableSquares = _selectedPiece.GetMovableSquares(false);
            foreach (var square in SelectedPieceMovableSquares)
            {
                square.Entity.GetComponent<Renderer>().Color = Color.Green;
            }
            
        }
    }

    public static ChessBoard ChessBoard { get; } = new ChessBoard();
    public static List<ChessPiece> BlackPieces { get; set; }
    public static List<ChessPiece> WhitePieces { get; set; }

    private static ChessColor _playerTurn = ChessColor.White;
    private static ChessPiece? _selectedPiece;

    public static IEnumerable<Square> SelectedPieceMovableSquares { get; private set; } = new List<Square>();
    


    static ChessManager()
    {
        ChessBoard.LoadBoard();
    }

    public static void LoadPieces()
    {
        var blackPiecesEntities = new List<EntityLogic.Entity>()
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
            blackPiecesEntities.Add(piece);
        }
        
        

        var whitePiecesEntities = new List<EntityLogic.Entity>()
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
            whitePiecesEntities.Add(piece);
        }

        foreach (var entity in blackPiecesEntities)
        {
            entity.GetComponent<Interactive>().SetInactive();
        }

        WhitePieces = whitePiecesEntities.Select(e => e.GetBehaviour<ChessPiece>()).ToList();
        BlackPieces = blackPiecesEntities.Select(e => e.GetBehaviour<ChessPiece>()).ToList();
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

        switch (SelectedPiece)
        {
            case PawnPiece pawnPiece:
                pawnPiece.SetHasMoved();
                break;
            case RookPiece rookPiece:
                rookPiece.HasMoved = true;
                break;
        }

        if (SelectedPiece is KingPiece kingPiece)
        {
            if (square.ChessPosition.Position.X > kingPiece.Pos.X + 1 && kingPiece.HasMoved == false)
            {
                var rookPiece = ChessBoard.Squares[kingPiece.Pos.Y, kingPiece.Pos.X + 3].OccupyingChessPiece;

                if (rookPiece is RookPiece {HasMoved: false } castleRookPiece )
                {
                    castleRookPiece.CurrentSquare = ChessBoard.Squares[kingPiece.Pos.Y, kingPiece.Pos.X + 1];
                }
            }
            kingPiece.HasMoved = true;
        }

        SelectedPiece.CurrentSquare = square;
        SelectedPiece = null;

        EndTurn();
    }

    private static void EndTurn()
    {
        if (_playerTurn == ChessColor.Black)
        {
            CheckMateCalculator.CalculateChecks(BlackPieces);
            SwitchActivePieces(BlackPieces, WhitePieces);
            CalculatePins(WhitePieces, BlackPieces);
            CheckMateCalculator.CalculateCheckMate(WhitePieces);
        }
        else
        {
            CheckMateCalculator.CalculateChecks(WhitePieces);
            SwitchActivePieces(WhitePieces, BlackPieces);
            CalculatePins(BlackPieces, WhitePieces);
            CheckMateCalculator.CalculateCheckMate(WhitePieces);
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