using System;
using MainProject.Entities;
using MainProject.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MainProject.Components;

public class Interactive : Component
{
    private readonly Renderer _renderer;
    private readonly Transform _transform;

    private bool _mouseIsDown;
    private Point MousePosition => Mouse.GetState().Position;

    public event Action OnMouseHoover;

    public event Action OnMouseEnter;

    public event Action OnMouseExit;

    public event Action OnLeftClick;

    public Interactive(Entity entity) : base(entity)
    {
        _renderer = entity.GetComponent<Renderer>();
        _transform = entity.GetComponent<Transform>();
        
        InteractiveSystem.UpdateInteractives();

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
            !_mouseIsDown && 
            Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            _mouseIsDown = true;
            return true;
        }

        return false;
    }
    

    public void Update()
    {
        if (Mouse.GetState().LeftButton == ButtonState.Released)
        {
            _mouseIsDown = false;
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