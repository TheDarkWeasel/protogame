

/**
 * Node, which is just waiting, unit the target has reached it's destination
 **/
public class WaitUntilArrivedAtDestinationNode : LeafNode
{
    private UnitNavigation unitNavigation;
    private IBlackboard blackboard;

    public WaitUntilArrivedAtDestinationNode(IBlackboard blackboard) : base(blackboard)
    {
        this.blackboard = blackboard;
        unitNavigation = blackboard.GetTargetGameObject().GetComponent<UnitNavigation>();
    }

    public override bool CheckConditions()
    {
        return unitNavigation != null;
    }

    public override void DoAction()
    {
        if (unitNavigation.HasReachedDestination())
        {
            control.FinishWithSuccess();
        }
    }

    public override void End()
    {
        //nothing here
    }

    public override void Start()
    {
        //nothing here
    }
}
