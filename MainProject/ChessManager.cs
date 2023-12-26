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

    private static ChessColor playerTurn = ChessColor.White;
    
    public static IEnumerable<Entity.Entity> SelectedPieceMovableSquares { get; private set; } = new List<Entity.Entity>();
    

    public static void SetMovableSquares(List<Entity.Entity> entities)
    {
        foreach (var entity in SelectedPieceMovableSquares)
        {
            entity.GetComponent<Renderer>().Color = Color.White;
        }

        foreach (var chessPin in ChessPins)
        {
            if (chessPin.PinnedPiece == SelectedPiece)
            {
                entities = entities.Where(e => chessPin.ViableSquares.Contains(e.GetBehaviour<Square>())).ToList();

            }
        }

        if (SelectedPiece is KingPiece kingPiece)
        {
            var list = playerTurn == ChessColor.Black ? WhitePieces : BlackPieces;
            foreach (var piece in list)
            {
                var attackedSquares = piece.GetBehaviour<ChessPiece>().GetThreats(kingPiece);

                foreach (var square in attackedSquares)
                {
                    if (entities.Contains(square.Entity))
                    {
                        entities.Remove(square.Entity);
                    }
                }
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
            BlackPieces.Add(piece);
        }
        
        

        WhitePieces = new List<Entity.Entity>()
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
            WhitePieces.Add(piece);
        }

        foreach (var entity in BlackPieces)
        {
            entity.GetComponent<Interactive>().SetInactive();
        }
        CalculatePins(BlackPieces);
        
    }

    private static void CalculatePins(List<Entity.Entity> pieces)
    {
        var pins = new List<ChessPin>();
        foreach (var piece in pieces)
        {
            var pin = piece.GetBehaviour<ChessPiece>().GetChessPin();

            if (pin is not null)
            {
                pins.Add(pin);
                
            }
        }

        ChessPins = pins;
    }

    private static void EndTurn()
    {
        if (playerTurn == ChessColor.Black)
        {
            foreach (var entity in BlackPieces)
            {
                entity.GetComponent<Interactive>().SetInactive();
            }
            foreach (var entity in WhitePieces)
            {
                entity.GetComponent<Interactive>().SetActive();
            }
            
        }
        else
        {
            foreach (var entity in WhitePieces)
            {
                entity.GetComponent<Interactive>().SetInactive();
            }
            foreach (var entity in BlackPieces)
            {
                entity.GetComponent<Interactive>().SetActive();
            }
        }
        playerTurn = playerTurn == ChessColor.Black ? ChessColor.White : ChessColor.Black;

    }

    public static void MoveSelectedPiece(Square square)
    {
        if (square.SquareState != SquareState.NotOccupied)
        {
            if (square.SquareState == SquareState.OccupiedByBlack)
            {
                BlackPieces.Remove(square.OccupyingChessPiece.Entity);
                square.OccupyingChessPiece.Entity.Destroy();
            }
            else
            {
                WhitePieces.Remove(square.OccupyingChessPiece.Entity);
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

        SetMovableSquares(new List<Entity.Entity>());
        EndTurn();
        if (playerTurn == ChessColor.White)
        {
            CalculatePins(BlackPieces);
        }
        else
        {
            CalculatePins(WhitePieces);
        }
    }
}