using MainProject.Components;
using MainProject.Entities;
using MainProject.Managers;
using Microsoft.Xna.Framework;

namespace MainProject.Factories;

public static class SquareFactory
{

    public static Entity CreateSquare(ChessColor chessColor, Point position, int squareSize)
    {
        var entity = EntityManager.CreateEntity();
        var square = new Square(position);
        square.Entity = entity;
        entity.AddBehaviour(square);
        entity.GetComponent<Renderer>().Texture = chessColor == ChessColor.White ? Textures.WhiteSquare : Textures.BlackSquare;
        entity.GetComponent<Transform>().Position = new Vector2(position.X * squareSize, position.Y * squareSize);
        return entity;
    }
    
    
}