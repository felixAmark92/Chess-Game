using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Components;

public class Renderer : IComponent
{
    public Texture2D Texture { get; set; }
    public Transform Transform { get; set; }

    public Color Color { get; set; } = Color.White;

    public Renderer()
    {
        
    }
    
    public Renderer(Transform transform)
    {
        Transform = transform;
    }

    public Renderer(Texture2D texture, Transform transform)
    {
        Texture = texture;
        Transform = transform;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Transform.Position, Color);
    }
}