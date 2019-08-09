using UnityEngine;
using System.Collections;

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
        return unitNavigation != null && blackboard.GetActionDestination() != null;
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
