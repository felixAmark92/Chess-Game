
using Microsoft.Xna.Framework;

namespace MainProject.BehaviourScripts;

public class ChessPosition
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