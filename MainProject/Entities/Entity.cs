using System;
using System.Collections.Generic;
using System.Linq;
using MainProject.Components;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Entities;

public abstract class Entity
{
    public List<IComponent> Components { get; } = new List<IComponent>();

    public T GetComponent<T>() where T : class, IComponent
    {
        return Components.OfType<T>().FirstOrDefault();
    }

    public Entity()
    {
        Components.Add(new Transform());
        Components.Add(new Renderer(GetComponent<Transform>()));
    }

    public void Destroy()
    {
        throw new NotImplementedException();
    }
}