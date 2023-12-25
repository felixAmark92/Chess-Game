using Microsoft.Xna.Framework;


namespace MainProject.Components;

public class Transform : Component
{
    public Vector2 Position { get; set; }   
    
    //for now no need for rotation and scale
    
    public Transform(Entity.Entity entity) : base(entity)
    {
    }
}