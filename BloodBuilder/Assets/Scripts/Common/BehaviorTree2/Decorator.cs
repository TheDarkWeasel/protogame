using UnityEngine;
using UnityEditor;

public abstract class Decorator : Node2
{
    Node2 node;

    public Decorator(IBlackboard bb, Node2 task) : base(bb)
    {
        InitNode(task);
    }

    private void InitNode(Node2 task)
    {
        this.node = task;
        this.node.GetControl().SetNode(this);
    }


    public override bool CheckConditions()
    {
        return this.node.CheckConditions();
    }

    public override void End()
    {
        this.node.End();
    }

    public override NodeController GetControl()
    {
        return this.node.GetControl();
    }

    public override void Start()
    {
        this.node.Start();
    }
}