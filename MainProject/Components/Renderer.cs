using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Components;

public class Renderer : IComponent
{
    private Texture2D _texture;
    private Transform _transform;

    public Renderer()
    {
        
    }
    
    public Renderer(Transform transform)
    {
        _transform = transform;
    }

    public Renderer(Texture2D texture, Transform transform)
    {
        _texture = texture;
        _transform = transform;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _transform.Position, Color.White);
    }
}