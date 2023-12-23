using System;
using System.Linq;
using System.Net.Mime;
using MainProject.Components;
using MainProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject;

public class ChessBoard : Entity
{
    public Square[,] Squares { get; } = new Square[8, 8];

    public int SquaresSize { get; set; } = 100;

    private Texture2D WhiteSquareTexture { get; set; }
    public Texture2D BlackSquareTexture { get; set; }

    public void LoadTexture(ContentManager content)
    {
        WhiteSquareTexture = content.Load<Texture2D>("WhiteSquare");
        BlackSquareTexture = content.Load<Texture2D>("BlackSquare");
    }
    public void LoadBoard()
    {

        var chessColor = BlackSquareTexture;
        
        for (int i = 0; i < Squares.GetLength(0); i++)
        {
            for (int j = 0; j < Squares.GetLength(1); j++)
            {
                Squares[i, j] = new Square(new Point(j, i), chessColor, SquaresSize);
                Squares[i, j].GetComponent<Renderer>().Texture = chessColor;
                Squares[i, j].GetComponent<Transform>().Position = new Vector2(j * SquaresSize, i * SquaresSize);
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

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int y = 0; y < Squares.GetLength(0); y++)
        {
            for (int x = 0; x < Squares.GetLength(1); x++)
            {
                Squares[y,x].GetComponent<Renderer>().Draw(spriteBatch);
            }
        }
    }
    
    private Texture2D ColorSwapped(Texture2D texture)
    {
        return texture == BlackSquareTexture ? WhiteSquareTexture : BlackSquareTexture;

    }
}