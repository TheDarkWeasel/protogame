
/**
 *  Moves the target GameObject to a destination. Only works, if the GameObject has the UnitNavigation-Component 
 **/
public class MoveToDestinationNode : LeafNode
{
    private UnitNavigation unitNavigation;
    private IBlackboard blackboard;

    public MoveToDestinationNode(IBlackboard blackboard) : base(blackboard)
    {
        this.blackboard = blackboard;
        unitNavigation = blackboard.GetTargetGameObject().GetComponent<UnitNavigation>();
    }

    public override bool CheckConditions()
    {
        return unitNavigation != null && blackboard.GetActionDestination() != null;
    }

    public override void DoAction()
    {
        if (unitNavigation.SetDestination(blackboard.GetActionDestination()))
        {
            control.FinishWithSuccess();
        }
        else
        {
            control.FinishWithFailure();
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