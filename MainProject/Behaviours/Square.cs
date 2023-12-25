using MainProject.Behaviours.ChessPieces;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.Behaviours;

public class Square : Behaviour
{
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
}
