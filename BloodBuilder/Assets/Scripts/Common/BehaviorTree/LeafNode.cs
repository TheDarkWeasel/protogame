using UnityEngine;
using UnityEditor;

public abstract class LeafNode : Node2
{

    protected NodeController control;

    public LeafNode(IBlackboard blackboard) : base(blackboard)
    {
        CreateController();
    }

    private void CreateController()
    {
        this.control = new NodeController(this);
    }

    public override NodeController GetControl()
    {
        return this.control;
    }
}