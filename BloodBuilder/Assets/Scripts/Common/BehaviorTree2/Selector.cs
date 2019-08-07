using UnityEngine;
using UnityEditor;

public class Selector : ParentNode
{

    public Selector(IBlackboard bb) : base(bb)
    {

    }

    public Node2 ChooseNewNode()
    {
        Node2 node = null;
        bool found = false;
        int curPos = control.subnodes.IndexOf(control.currentNode);
        while (!found)
        {
            if (curPos == (control.subnodes.Count - 1))
            {
                found = true;
                node = null;
                break;
            }
            curPos++;
            node = control.subnodes[curPos];
            if (node.CheckConditions())
            {
                found = true;
            }
        }
        return node;
    }

    public override void ChildFailed()
    {
        control.currentNode = ChooseNewNode();
        if (control.currentNode == null)
        {
            control.FinishWithFailure();
        }
    }

    public override void ChildSucceeded()
    {
        control.FinishWithSuccess();
    }
}