using MainProject.Entities;
using MainProject.Managers;

namespace MainProject;

public class Component
{
    private readonly Entity _entity;

    public Component(Entity entity)
    {
        _entity = entity;
    }

}