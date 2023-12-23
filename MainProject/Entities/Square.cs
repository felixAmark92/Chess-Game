using MainProject.Components;
using MainProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject;

public class Square : Entity
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
    
    
    public Square(Point position, Texture2D squareColor, int squareSize)
    {
        var transform = new Transform();
        transform.Position = new Vector2(position.X * squareSize, position.Y * squareSize);

        ChessPosition = new ChessPosition(position);
        
        Components.Add(ChessPosition);
        Components.Add(transform);
        Components.Add(new Renderer(squareColor, transform));
        
    }
}
