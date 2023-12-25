using MainProject.Enums;
using MainProject.Factories;
using Microsoft.Xna.Framework;

namespace MainProject.Behaviours;

public class ChessBoard
{
    public Square[,] Squares { get; } = new Square[8, 8];

    public int SquaresSize { get; set; } = 100;
    
    public void LoadBoard()
    {

        var chessColor = ChessColor.Black;
        
        for (int i = 0; i < Squares.GetLength(0); i++)
        {
            for (int j = 0; j < Squares.GetLength(1); j++)
            {
                var squareEntity = SquareFactory.CreateSquare(chessColor, new Point(j, i), SquaresSize);
                Squares[i, j] = squareEntity.GetBehaviour<Square>();
                chessColor = ColorSwapped(chessColor);
            }
            chessColor = ColorSwapped(chessColor);
        }
    }

    public bool InsideChessBoard(Point point)
    {
        return point.X >= 0 &&
               point.X < Squares.GetLength(1) &&
               point.Y >= 0 &&
               point.Y < Squares.GetLength(0);
    }
    
    private ChessColor ColorSwapped(ChessColor chessColor)
    {
        return chessColor == ChessColor.Black ? ChessColor.White : ChessColor.Black;

    }
}