using System;
using MainProject.Components;
using MainProject.Entities;
using MainProject.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Builders;

public class EntityBuilder
{
    private Entity _entity;


    private EntityBuilder()
    {
        _entity = EntityManager.CreateEntity();
    }

    public static EntityBuilder Create()
    {
        return new EntityBuilder();
    }

    public EntityBuilder WithComponent<T>() where T : Component
    {
        var component = (T)Activator.CreateInstance(typeof(T), _entity);
        _entity.AddComponent(component);
        
        return this;
    }

    public EntityBuilder AddTexture(Texture2D texture)
    {
        _entity.GetComponent<Renderer>().Texture = texture;
        
        return this;
    }

    public EntityBuilder AddBehaviour(Behaviour behaviour)
    {
        _entity.AddBehaviour(behaviour);
        behaviour.Entity = _entity;
        behaviour.ComponentsInit();

        return this;
    }

    public Entity Build()
    {
        return _entity;
    }
    

}