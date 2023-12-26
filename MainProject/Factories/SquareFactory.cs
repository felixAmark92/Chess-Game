using MainProject.BehaviourScripts;
using MainProject.Builders;
using MainProject.Components;
using MainProject.EntityLogic;
using MainProject.Enums;
using Microsoft.Xna.Framework;

namespace MainProject.Factories;

public static class SquareFactory
{

    public static EntityLogic.Entity CreateSquare(ChessColor chessColor, Point position, int squareSize)
    {
        var entity = EntityManager.CreateEntity();
        var square = new Square(position);
        square.Entity = entity;
        entity.AddBehaviour(square);
        entity.GetComponent<Renderer>().Texture = chessColor == ChessColor.White ? ChessTextures.WhiteSquare : ChessTextures.BlackSquare;
        entity.GetComponent<Transform>().Position = new Vector2(position.X * squareSize, position.Y * squareSize);
        entity.AddComponent(new Interactive(entity));
        square.ComponentsInit();
        return entity;
    }
    
    
}