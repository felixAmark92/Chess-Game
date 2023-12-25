using System;
using System.Collections.Generic;
using System.Linq;
using MainProject.ChessMovements;
using MainProject.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Entities;

public abstract class ChessPiece : Behaviour
{
     private readonly ChessBoard _chessBoard;
     private Square _currentSquare;
     
     protected IChessMovement ChessMovement { get; set; }
     
     public ChessColor ChessColor { get; }

     public ChessPosition ChessPosition => CurrentSquare.ChessPosition;

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

          Entity.GetComponent<Interactive>().OnLeftClick += () =>
          {
               Console.WriteLine(Entity.Id);
               
               var list = GetMovableSquares().Select(s => s.Entity);
               
               Highlighter.HighlightEntities(list);
          };
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