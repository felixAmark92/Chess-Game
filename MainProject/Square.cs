using System.Drawing;

namespace MainProject;

public class Square : IChessPosition
{
    public Point Position { get; set; }

    public ChessColor ChessColor { get; set; }

    public Square(Point position, ChessColor chessColor)
    {
        Position = position;
        ChessColor = chessColor;
    }
}
