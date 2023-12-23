using MainProject.ChessMovements;
using MainProject.Components;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Entities;

public abstract class ChessPiece : Entity
{
     private Square _currentSquare;
     
     public IChessMovement ChessMovement { get; }
     
     public ChessColor ChessColor { get; }

     public ChessPosition ChessPosition => CurrentSquare.ChessPosition;

     public ChessPiece(ChessColor chessColor)
     {
          ChessColor = chessColor;
     }

     public Square CurrentSquare
     {
          get => _currentSquare;
          set
          {
               _currentSquare.OccupyingChessPiece = null;
               _currentSquare = value;
               _currentSquare.OccupyingChessPiece = this;

          }
     }

}