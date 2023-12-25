using System;
using System.Collections.Generic;
using System.Linq;
using MainProject.Components;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Entities;

public class Entity
{
    public ulong Id { get; set; }
    private List<Component> Components { get; } = new List<Component>();
    private List<Behaviour> Behaviours { get; } = new List<Behaviour>();

    public T GetComponent<T>() where T : Component
    {
        return Components.OfType<T>().FirstOrDefault();
    }
    public bool HasComponent<T>() where T : Component
    {
        return Components.Any(c => c is T);
    }

    public void AddComponent(Component component)
    {
        Components.Add(component);
    }

    public void AddBehaviour(Behaviour behaviour)
    {
        Behaviours.Add(behaviour);
    }
    public T GetBehaviour<T>() where T : Behaviour
    {
        return Behaviours.OfType<T>().FirstOrDefault();
    }
    
    

    public Entity(ulong id)
    {
        Id = id;
        Components.Add(new Transform(this));
        Components.Add(new Renderer(this));
    }

    public void Destroy()
    {
        throw new NotImplementedException();
    }
}