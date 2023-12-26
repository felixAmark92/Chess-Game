using System.Collections.Generic;
using System.Linq;
using MainProject.Components;

namespace MainProject.EntityLogic;

public static class EntityManager
{
    private static List<EntityLogic.Entity> _entities = new List<EntityLogic.Entity>();

    private static ulong IdCreator = 0;
    
    
    public static EntityLogic.Entity CreateEntity()
    {
        var entity = new EntityLogic.Entity(IdCreator);
        _entities.Add(entity);
        IdCreator++;
        return entity;
    }

    public static void DestroyEntity(EntityLogic.Entity entity)
    {
        _entities.Remove(entity);
        // Additional cleanup logic if needed
    }

    public static IEnumerable<EntityLogic.Entity> GetEntitiesWithComponent<T>() where T : Component
    {
        return _entities.Where(entity => entity.HasComponent<T>());
    }
    
    

}