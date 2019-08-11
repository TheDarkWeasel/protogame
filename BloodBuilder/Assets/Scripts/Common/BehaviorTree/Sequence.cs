using UnityEngine;
using UnityEditor;

/* returns FAILURE, if one child fails,
   returns SUCCESS, if all children succeed
   returns RUNNING, if not all children have finished */
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