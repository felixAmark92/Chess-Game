using System.Collections.Generic;
using MainProject.BehaviourScripts.ChessPieces;
using MainProject.Enums;
using MainProject.Factories;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts;

public class ChessBoard
{
    public Square[,] Squares { get; } = new Square[8, 8];

    public int SquaresSize { get; set; } = 100;
    
    public void LoadBoard()
    {

        var chessColor = ChessColor.Black;
        
        for (int i = 0; i < Squares.GetLength(0); i++)
        {
            for (int j = 0; j < Squares.GetLength(1); j++)
            {
                var squareEntity = SquareFactory.CreateSquare(chessColor, new Point(j, i), SquaresSize);
                Squares[i, j] = squareEntity.GetBehaviour<Square>();
                chessColor = ColorSwap(chessColor);
            }
            chessColor = ColorSwap(chessColor);
        }
    }

    public bool InsideChessBoard(Point point)
    {
        return point.X >= 0 &&
               point.X < Squares.GetLength(1) &&
               point.Y >= 0 &&
               point.Y < Squares.GetLength(0);
    }
    
    private ChessColor ColorSwap(ChessColor chessColor)
    {
        return chessColor == ChessColor.Black ? ChessColor.White : ChessColor.Black;

    }
    
    
    
    
    
    
    
    //
    // public static void SetMovableSquares(List<EntityLogic.Entity> entities)
    // {
    //     foreach (var entity in SelectedPieceMovableSquares)
    //     {
    //         entity.GetComponent<Renderer>().Color = Color.White;
    //     }
    //
    //     foreach (var chessPin in ChessPins)
    //     {
    //         if (chessPin.PinnedPiece == SelectedPiece)
    //         {
    //             entities = entities.Where(e => chessPin.ViableSquares.Contains(e.GetBehaviour<Square>())).ToList();
    //
    //         }
    //     }
    //
    //     if (SelectedPiece is KingPiece kingPiece)
    //     {
    //         var list = playerTurn == ChessColor.Black ? WhitePieces : BlackPieces;
    //         foreach (var piece in list)
    //         {
    //             var attackedSquares = piece.GetBehaviour<ChessPiece>().GetThreats(kingPiece);
    //
    //             foreach (var square in attackedSquares)
    //             {
    //                 if (entities.Contains(square.Entity))
    //                 {
    //                     entities.Remove(square.Entity);
    //                 }
    //             }
    //         }
    //     }
    //     
    //     foreach (var newEntity in entities)
    //     {
    //         newEntity.GetComponent<Renderer>().Color = Color.Green;
    //     }
    //     
    //     SelectedPieceMovableSquares = entities;
    // }
    //
    // private static void CalculatePins(List<EntityLogic.Entity> pieces)
    // {
    //     var pins = new List<ChessPin>();
    //     foreach (var piece in pieces)
    //     {
    //         var pin = piece.GetBehaviour<ChessPiece>().GetChessPin();
    //
    //         if (pin is not null)
    //         {
    //             pins.Add(pin);
    //             
    //         }
    //     }
    //
    //     ChessPins = pins;
    // }
    //
    // private static void CalculatePins(List<EntityLogic.Entity> pieces)
    // {
    //     var pins = new List<ChessPin>();
    //     foreach (var piece in pieces)
    //     {
    //         var pin = piece.GetBehaviour<ChessPiece>().GetChessPin();
    //
    //         if (pin is not null)
    //         {
    //             pins.Add(pin);
    //             
    //         }
    //     }
    //
    //     ChessPins = pins;
    // }
}