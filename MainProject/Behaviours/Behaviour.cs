using MainProject.Entities;
using MainProject.Managers;

namespace MainProject.Components;

public abstract class Behaviour
{
    public Entity Entity { get; set; }

    public Behaviour()
    {
    }

    public virtual void ComponentsInit()
    {
        
    }

    //is this needed?
}