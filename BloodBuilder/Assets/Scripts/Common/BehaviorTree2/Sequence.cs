using UnityEngine;
using UnityEditor;

public class Sequence : ParentNode
{

    public Sequence(IBlackboard bb) : base(bb)
    {

    }

    public override void ChildFailed()
    {
        control.FinishWithFailure();
    }

    public override void ChildSucceeded()
    {
        int curPos = control.subnodes.IndexOf(control.currentNode);
        if (curPos == (control.subnodes.Count - 1))
        {
            control.FinishWithSuccess();
        }
        else
        {
            control.currentNode = control.subnodes[curPos + 1];
            if (!control.currentNode.CheckConditions())
            {
                control.FinishWithFailure();
            }
        }
    }
}