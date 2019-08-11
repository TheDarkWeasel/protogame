using UnityEngine;
using UnityEditor;

public abstract class ParentNode : Node2
{
    protected ParentNodeController control;

    public ParentNode(IBlackboard bb) : base(bb)
    {
        CreateController();
    }

    private void CreateController()
    {
        this.control = new ParentNodeController(this);
    }

    public override NodeController GetControl()
    {
        return control;
    }

    public void Add(Node2 task)
    {
        control.Add(task);
    }

    public override bool CheckConditions()
    {
        return control.subnodes.Count > 0;
    }

    public abstract void ChildSucceeded();

    public abstract void ChildFailed();


    public override void DoAction()
    {
        if (control.Finished())
        {
            return;
        }
        if (control.currentNode == null)
        {
            return;
        }

        if (!control.currentNode.GetControl().Started() && !control.currentNode.GetControl().Finished())
        {
            control.currentNode.GetControl().SafeStart();
        }
        else if (control.currentNode.GetControl().Finished())
        {
            if (control.currentNode.GetControl().Succeeded())
            {
                this.ChildSucceeded();
            }
            else if (control.currentNode.GetControl().Failed())
            {
                this.ChildFailed();
            }
            control.currentNode.GetControl().SafeEnd();
        }
        else
        {
            control.currentNode.DoAction();
        }
    }

    public override void End()
    {
    }

    public override void Start()
    {
        control.Reset();
        if (control.currentNode == null)
        {
            throw new IllegalStateException("No current task?");
        }
    }
}