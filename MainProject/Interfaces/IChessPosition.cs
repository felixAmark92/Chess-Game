using System.Drawing;

namespace MainProject;

public interface IChessPosition
{

    public Point Position { get; set; }

    public string ChessPosition => $"{'a' + Position.X}{8 - Position.Y}";
}