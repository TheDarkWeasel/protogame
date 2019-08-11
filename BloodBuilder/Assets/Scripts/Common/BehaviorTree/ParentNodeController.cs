using System.Collections.Generic;

public class ParentNodeController : NodeController
{
    public List<Node2> subnodes;
    public Node2 currentNode;

    public ParentNodeController(Node2 task) : base(task)
    {
        this.subnodes = new List<Node2>();
        this.currentNode = null;
    }

    public void Add(Node2 task)
    {
        subnodes.Add(task);
    }

    public override void Reset()
    {
        base.Reset();
        if (subnodes.Count > 0)
        {
            this.currentNode = subnodes[0];
        }
        else
        {
            this.currentNode = null;
        }
    }
}