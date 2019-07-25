using System.Collections.Generic;

public class SequenceNode : Node
{
    private List<Node> m_nodes = new List<Node>();

    public SequenceNode(List<Node> nodes)
    {
        m_nodes = nodes;
    }

    /* returns FAILURE, if one child fails,
       returns SUCCESS, if all children succeed
       returns RUNNING, if not all children have finished */
    public override NodeState Evaluate()
    {
        foreach (Node node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    m_nodeState = NodeState.FAILURE;
                    return m_nodeState;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNING:
                    m_nodeState = NodeState.RUNNING;
                    return m_nodeState;
                default:
                    throw new IllegalStateException("You should not be here!");
            }
        }
        m_nodeState = NodeState.SUCCESS;
        return m_nodeState;
    }
}