
using Microsoft.Xna.Framework;

namespace MainProject.Components;

public class ChessPosition : IComponent
{
    public Point Position { get; set; }

    public ChessPosition()
    {
        
    }

    public ChessPosition(Point position)
    {
        Position = position;
    }

    public string Board
    {
        get
        {
            return $"{(char)('a' + Position.X)}{8 - Position.Y}";
        }
    } 
}