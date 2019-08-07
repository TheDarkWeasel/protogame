public class NodeController
{

    private bool done;
    private bool sucess;
    private bool started;
    private Node2 node;

    public NodeController(Node2 task)
    {
        SetNode(task);
        Initialize();
    }

    private void Initialize()
    {
        this.done = false;
        this.sucess = true;
        this.started = false;
    }

    public void SetNode(Node2 task)
    {
        this.node = task;
    }

    public void SafeStart()
    {
        this.started = true;
        node.Start();
    }

    public void SafeEnd()
    {
        this.done = false;
        this.started = false;
        node.End();
    }

    public void FinishWithSuccess()
    {
        this.sucess = true;
        this.done = true;
    }

    public void FinishWithFailure()
    {
        this.sucess = false;
        this.done = true;
    }

    public bool Succeeded()
    {
        return this.sucess;
    }

    public bool Failed()
    {
        return !this.sucess;
    }

    public bool Finished()
    {
        return this.done;
    }

    public bool Started()
    {
        return this.started;
    }

    public virtual void Reset()
    {
        this.done = false;
    }
}