using UnityEngine;
using UnityEditor;

public class CalculateNextFreePositionNode : LeafNode
{
    private IBlackboard blackboard;

    public CalculateNextFreePositionNode(IBlackboard blackboard) : base(blackboard)
    {
        this.blackboard = blackboard;
    }

    public override bool CheckConditions()
    {
        return blackboard.GetActionDestination() != null;
    }

    public override void DoAction()
    {
        int tries = 100;

        while (tries > 0)
        {
            Vector3 spawnPos = blackboard.GetActionDestination();
            float radius = 1.0f;

            if (Physics.CheckSphere(spawnPos, radius))
            {
                float x = blackboard.GetActionDestination().x;
                float z = blackboard.GetActionDestination().z;

                //TODO generate new vector with minor random differences

                tries--;
            }
            else
            {
                control.FinishWithSuccess();
                return;
            }
        }

        control.FinishWithFailure();
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