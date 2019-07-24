
[System.Serializable]
public abstract class Node
{
    public delegate NodeState NodeReturn();

    protected NodeState m_nodeState;

    public NodeState nodeState
    {
        get { return m_nodeState; }
    }

    public Node() { }

    public abstract NodeState Evaluate();
}