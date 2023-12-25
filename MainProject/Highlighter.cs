using System.Collections;
using System.Collections.Generic;
using MainProject.Components;
using Microsoft.Xna.Framework;

namespace MainProject;

public static class Highlighter
{
    private static IEnumerable<Entity.Entity> _higlightedEntities = new List<Entity.Entity>();

    public static void HighlightEntities(IEnumerable<Entity.Entity> entities)
    {
        foreach (var entity in _higlightedEntities)
        {
            entity.GetComponent<Renderer>().Color = Color.White;
        }

        foreach (var newEntity in entities)
        {
            newEntity.GetComponent<Renderer>().Color = Color.Green;
        }

        _higlightedEntities = entities;
    }
    
}