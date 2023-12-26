namespace MainProject.BehaviourScripts;

public abstract class Behaviour
{
    public EntityLogic.Entity Entity { get; set; }

    public Behaviour()
    {
    }

    public virtual void ComponentsInit()
    {
        
    }

}