using System;
using System.Collections.Generic;
using System.Linq;
using MainProject.ChessMovements;
using MainProject.ChessMovements.ChessPins;
using MainProject.Components;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.Behaviours.ChessPieces;

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
               
               var list = GetMovableSquares().Select(s => s.Entity).ToList();

               ChessManager.SelectedPiece = this;
               ChessManager.SetMovableSquares(list);
          };
     }

     public List<Square> GetMovableSquares()
     {
          return ChessMovement.GetMovableSquares();
     }

     public IEnumerable<Square> GetThreats(KingPiece kingPiece)
     {
          var kingSquares = kingPiece.GetMovableSquares();
          var thisSquares = GetMovableSquares();

          return kingSquares.Where(s => thisSquares.Contains(s));
     }
     public ChessPin? GetChessPin()
     {
          return _pinCalculator.CalculatePin();
     }
     

     

}