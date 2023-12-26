using MainProject.EntityLogic;

namespace MainProject.BehaviourScripts;

public abstract class Behaviour
{
    public Entity Entity { get; set; }

    public Behaviour()
    {
    }

    public virtual void ComponentsInit()
    {
        
    }

}