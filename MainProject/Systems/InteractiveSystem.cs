using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MainProject.Components;
using MainProject.Entity;

namespace MainProject.Systems;

public static class InteractiveSystem
{
    private static IEnumerable<Interactive> _interactives = new List<Interactive>();
    
    public static bool MouseIsDown { get; set; }


    public static void UpdateInteractives()
    {
        var entities = 
            EntityManager
                .GetEntitiesWithComponent<Interactive>()
                .OrderByDescending(e => e.GetComponent<Renderer>().LayerDepth);
        
        _interactives = entities.Select(e => e.GetComponent<Interactive>());
    }

    public static void Update()
    {
        foreach (var interactive in _interactives)
        {
            interactive.Update();
        }
    }
    
}