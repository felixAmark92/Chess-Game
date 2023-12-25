using System.Collections.Generic;
using System.Linq;
using MainProject.Components;

namespace MainProject.Entity;

public static class EntityManager
{
    private static List<Entity> _entities = new List<Entity>();

    private static ulong IdCreator = 0;
    
    
    public static Entity CreateEntity()
    {
        var entity = new Entity(IdCreator);
        _entities.Add(entity);
        IdCreator++;
        return entity;
    }

    public static void DestroyEntity(Entity entity)
    {
        _entities.Remove(entity);
        // Additional cleanup logic if needed
    }

    public static IEnumerable<Entity> GetEntitiesWithComponent<T>() where T : Component
    {
        return _entities.Where(entity => entity.HasComponent<T>());
    }
    
    

}