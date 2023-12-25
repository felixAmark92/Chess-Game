using System;
using System.Collections.Generic;
using System.Linq;
using MainProject.Behaviours;
using MainProject.Behaviours.ChessPieces;
using MainProject.ChessMovements.ChessPins;
using MainProject.Components;
using MainProject.Enums;
using MainProject.Factories;
using Microsoft.Xna.Framework;

namespace MainProject;

public static class ChessManager
{
    public static ChessPiece SelectedPiece { get; set; }
    public static ChessBoard ChessBoard { get; } = new ChessBoard();

    private static IEnumerable<ChessPin> ChessPins = new List<ChessPin>();

    private static List<Entity.Entity> BlackPieces { get; set; }
    private static List<Entity.Entity> WhitePieces { get; set; }
    
    
    
    public static IEnumerable<Entity.Entity> SelectedPieceMovableSquares { get; private set; } = new List<Entity.Entity>();

    public static void SetMovableSquares(IEnumerable<Entity.Entity> entities)
    {
        foreach (var entity in SelectedPieceMovableSquares)
        {
            entity.GetComponent<Renderer>().Color = Color.White;
        }

        foreach (var chessPin in ChessPins)
        {
            if (chessPin.PinnedPiece == SelectedPiece)
            {
                entities = entities.Where(e => chessPin.ViableSquares.Contains(e.GetBehaviour<Square>()));

            }
        }

        foreach (var newEntity in entities)
        {
            newEntity.GetComponent<Renderer>().Color = Color.Green;
        }
        
        SelectedPieceMovableSquares = entities;
    }

    static ChessManager()
    {
        ChessBoard.LoadBoard();



    }

    public static void LoadPieces()
    {
        BlackPieces = new List<Entity.Entity>()
        {
            ChessPieceFactory.CreateChessPiece(ChessType.Rook, ChessColor.Black, new Point(5, 1))
        };

        WhitePieces = new List<Entity.Entity>()
        {
            ChessPieceFactory.CreateChessPiece(ChessType.Rook, ChessColor.White, new Point(5, 5)),
            ChessPieceFactory.CreateChessPiece(ChessType.Bishop, ChessColor.White, new Point(1, 5)),
            ChessPieceFactory.CreateChessPiece(ChessType.King, ChessColor.White, new Point(5, 7))

        };

        foreach (var entity in BlackPieces)
        {
            entity.GetComponent<Interactive>().SetInactive();
        }
        CalculatePins();
        
    }

    private static void CalculatePins()
    {
        var pins = new List<ChessPin>();
        foreach (var blackPiece in BlackPieces)
        {
            var pin = blackPiece.GetBehaviour<ChessPiece>().GetChessPin();

            if (pin is not null)
            {
                pins.Add(pin);
                
            }
        }

        ChessPins = pins;
    }

    public static void MoveSelectedPiece(Square square)
    {
        if (square.SquareState != SquareState.NotOccupied)
        {
            square.OccupyingChessPiece.Entity.Destroy();
        }
        SelectedPiece.CurrentSquare = square;
        SelectedPiece.Entity.GetComponent<Transform>().Position = new Vector2(
            square.ChessPosition.Position.X * ChessBoard.SquaresSize,
            square.ChessPosition.Position.Y * ChessBoard.SquaresSize);

        SetMovableSquares(new List<Entity.Entity>());
        CalculatePins();
    }
}