using MainProject.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Components;

public class Renderer : Component
{
    public Texture2D Texture { get; set; }
    public Color Color { get; set; } = Color.White;


    public Renderer(Entity.Entity entity) : base(entity)
    {
        RenderingSystem.NotifyChanges();
    }
}