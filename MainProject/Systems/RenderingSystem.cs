﻿using System;
using System.Collections.Generic;
using System.Linq;
using MainProject.Components;
using MainProject.EntityLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MainProject.Systems;

public static class RenderingSystem
{
    private static IEnumerable<EntityLogic.Entity> _renderableEntities;
    
    public static void NotifyChanges()
    {
        var renderableEntities = EntityManager.GetEntitiesWithComponent<Renderer>();

        _renderableEntities = renderableEntities.Where(e => e.HasComponent<Transform>());

    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var entity in _renderableEntities)
        {
            var renderer = entity.GetComponent<Renderer>();
            var transform = entity.GetComponent<Transform>();
            
            //spriteBatch.Draw(renderer.Texture, transform.Position, renderer.Color);
            spriteBatch.Draw(
                renderer.Texture, 
                transform.Position, 
                null,
                renderer.Color,
                0f,
                new Vector2(),
                1,
                SpriteEffects.None,
                renderer.LayerDepth);
        }
    }
}