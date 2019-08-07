public abstract class Node2
{
    protected IBlackboard bb;

    public Node2(IBlackboard blackboard)
    {
        this.bb = blackboard;
    }

    public abstract bool CheckConditions();

    public abstract void Start();

    public abstract void End();

    public abstract void DoAction();

    public abstract NodeController GetControl();
}