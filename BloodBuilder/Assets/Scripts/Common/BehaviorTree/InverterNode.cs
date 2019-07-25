public class InverterNode : Node
{
    private Node m_node;

    public Node node
    {
        get { return m_node; }
    }

    public InverterNode(Node node)
    {
        m_node = node;
    }

    /* returns FAILURE, if node succeeds,
       returns SUCCESS, if node fails,
       returns RUNNING, if node is running */
    public override NodeState Evaluate()
    {
        switch (m_node.Evaluate())
        {
            case NodeState.FAILURE:
                m_nodeState = NodeState.SUCCESS;
                return m_nodeState;
            case NodeState.SUCCESS:
                m_nodeState = NodeState.FAILURE;
                return m_nodeState;
            case NodeState.RUNNING:
                m_nodeState = NodeState.RUNNING;
                return m_nodeState;
        }
        throw new IllegalStateException("You should not be here!");
    }
}