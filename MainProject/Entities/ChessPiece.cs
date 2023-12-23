using System.Collections.Generic;
using MainProject.ChessMovements;
using MainProject.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Entities;

public abstract class ChessPiece : Entity
{
     private Square _currentSquare;
     
     protected IChessMovement ChessMovement { get; set; }
     
     public ChessColor ChessColor { get; }

     public ChessPosition ChessPosition => CurrentSquare.ChessPosition;

     public ChessPiece(ChessColor chessColor, ChessBoard chessBoard, Point pos)
     {
          ChessColor = chessColor;
          CurrentSquare = chessBoard.Squares[pos.Y, pos.X];
          GetComponent<Transform>().Position = new Vector2(pos.X * chessBoard.SquaresSize, pos.Y * chessBoard.SquaresSize);
     }

     public List<Square> GetMovableSquares()
     {
          return ChessMovement.GetMovableSquares();
     }
     

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

}