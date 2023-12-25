using System;
using MainProject.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MainProject.Components;

public class Interactive : Component
{
    private bool _inactive { get; set; }
    private readonly Renderer _renderer;
    private readonly Transform _transform;
    private Point MousePosition => Mouse.GetState().Position;

    public event Action OnMouseHoover;

    public event Action OnMouseEnter;

    public event Action OnMouseExit;

    public event Action OnLeftClick;

    public Interactive(Entity.Entity entity) : base(entity)
    {
        if (!entity.HasComponent<Renderer>())
        {
            throw new MissingComponentException($"{typeof(Interactive)} is dependent on {typeof(Renderer)}");
        }

        if (!entity.HasComponent<Transform>())
        {
            throw new MissingComponentException($"{typeof(Interactive)} is dependent on {typeof(Transform)}");
        }
        _renderer = entity.GetComponent<Renderer>();
        _transform = entity.GetComponent<Transform>();
        
        InteractiveSystem.UpdateInteractives();

    }

    public void SetInactive()
    {
        _inactive = true;
    }

    public void SetActive()
    {
        _inactive = false;
    }

    private bool MouseHoover()
    {
        return MousePosition.X >= _transform.Position.X &&
               MousePosition.X <= _transform.Position.X + _renderer.Texture.Width &&
               MousePosition.Y >= _transform.Position.Y &&
               MousePosition.Y <= _transform.Position.Y + _renderer.Texture.Height;
    }

    private bool LeftMouseClicked()
    {
        if (MouseHoover() && 
            !InteractiveSystem.MouseIsDown && 
            Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            InteractiveSystem.MouseIsDown = true;
            return true;
        }

        return false;
    }
    

    public void Update()
    {
        if (_inactive)
        {
            return;
        }
        
        if (Mouse.GetState().LeftButton == ButtonState.Released)
        {
            InteractiveSystem.MouseIsDown = false;
        }
        if (MouseHoover())
        {
            OnMouseHoover?.Invoke();
        }

        if (LeftMouseClicked())
        {
            OnLeftClick?.Invoke();
        }
    }
}