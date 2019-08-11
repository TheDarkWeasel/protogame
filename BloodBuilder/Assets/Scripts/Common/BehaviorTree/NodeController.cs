public class NodeController
{

    private NodeState currentState = NodeState.IDLE;
    private Node2 node;

    public NodeController(Node2 node)
    {
        SetNode(node);
        Initialize();
    }

    private void Initialize()
    {
        currentState = NodeState.IDLE;
    }

    public void SetNode(Node2 node)
    {
        this.node = node;
    }

    public void SafeStart()
    {
        currentState = NodeState.RUNNING;
        node.Start();
    }

    public void SafeEnd()
    {
        currentState = NodeState.IDLE;
        node.End();
    }

    public void FinishWithSuccess()
    {
        currentState = NodeState.SUCCESS;
    }

    public void FinishWithFailure()
    {
        currentState = NodeState.FAILURE;
    }

    public bool Succeeded()
    {
        return currentState == NodeState.SUCCESS;
    }

    public bool Failed()
    {
        return currentState == NodeState.FAILURE;
    }

    public bool Finished()
    {
        return Succeeded() || Failed();
    }

    public bool Started()
    {
        return currentState == NodeState.RUNNING;
    }

    public virtual void Reset()
    {
        currentState = NodeState.RUNNING;
    }
}