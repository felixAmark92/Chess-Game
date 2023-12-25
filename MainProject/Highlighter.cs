using System.Collections;
using System.Collections.Generic;
using MainProject.Components;
using MainProject.Entities;
using Microsoft.Xna.Framework;

namespace MainProject;

public static class Highlighter
{
    private static IEnumerable<Entity> _higlightedEntities = new List<Entity>();

    public static void HighlightEntities(IEnumerable<Entity> entities)
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