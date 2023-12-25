namespace MainProject.Behaviours;

public abstract class Behaviour
{
    public Entity.Entity Entity { get; set; }

    public Behaviour()
    {
    }

    public virtual void ComponentsInit()
    {
        
    }

}