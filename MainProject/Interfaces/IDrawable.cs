using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject;

public interface IDrawable
{
    void LoadTexture(ContentManager content);
    void Draw(SpriteBatch spriteBatch);
}