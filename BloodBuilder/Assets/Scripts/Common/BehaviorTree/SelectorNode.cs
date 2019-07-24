using System.Collections.Generic;

public class SelectorNode : Node
{
    /** Selector child nodes */
    protected List<Node> m_nodes = new List<Node>();

    public SelectorNode(List<Node> nodes)
    {
        m_nodes = nodes;
    }

    /* returns FAILURE, if all children fail,
       returns SUCCESS, if one child succeeds
       returns RUNNING, if not all children have finished */
    public override NodeState Evaluate()
    {
        foreach (Node node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.SUCCESS:
                    m_nodeState = NodeState.SUCCESS;
                    return m_nodeState;
                case NodeState.RUNNING:
                    m_nodeState = NodeState.RUNNING;
                    return m_nodeState;
                default:
                    continue;
            }
        }
        m_nodeState = NodeState.FAILURE;
        return m_nodeState;
    }
}