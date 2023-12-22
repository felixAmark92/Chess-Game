using System;
using System.Drawing;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject;

public class ChessBoard : IDrawable
{
    public Square[,] Squares { get; } = new Square[8, 8];

    public int SquaresSize { get; set; } = 100;


    public ChessBoard()
    {
        var chessColor = ChessColor.Black;
        
        for (int i = 0; i < Squares.GetLength(0); i++)
        {
            for (int j = 0; j < Squares.GetLength(1); j++)
            {
                Squares[i, j] = new Square(new Point(j, i), chessColor);

                chessColor = ColorSwapped(chessColor);
            }
            chessColor = ColorSwapped(chessColor);
        }
    }

    private ChessColor ColorSwapped(ChessColor color)
    {
        return color == ChessColor.Black ? ChessColor.White : ChessColor.Black;

    }


    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Squares.GetLength(0); i++)
        {
            for (int j = 0; j < Squares.GetLength(1); j++)
            {
                spriteBatch.Draw();
            }
        }
    }
}