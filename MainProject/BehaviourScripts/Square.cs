using System;
using System.Linq;
using MainProject.BehaviourScripts.ChessPieces;
using MainProject.Components;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts;

public class Square : Behaviour
{
    public int X => ChessPosition.Position.X;
    public int Y => ChessPosition.Position.Y;
    public ChessPosition ChessPosition  { get; }
    public ChessPiece OccupyingChessPiece { get; set; }

    public SquareState SquareState => GetSquareState();



    private SquareState GetSquareState()
    {
        if (OccupyingChessPiece is null)
        {
            return SquareState.NotOccupied;
        }
        else if (OccupyingChessPiece.ChessColor == ChessColor.Black)
        {
            return SquareState.OccupiedByBlack;
        }

        return SquareState.OccupiedByWhite;
    }
    
    
    public Square(Point position)
    {
 
        ChessPosition = new ChessPosition(position);
        
    }

    public override void ComponentsInit()
    {
        Entity.GetComponent<Interactive>().OnLeftClick += () =>
        {
            Console.WriteLine(Entity.Id);
            if (ChessManager.SelectedPieceMovableSquares.Contains(Entity.GetBehaviour<Square>()))
            {
                ChessManager.MoveSelectedPiece(this);
            }
        };
    }
}
