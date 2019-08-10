using UnityEngine;

/**
 * Finds the next free spot near the action destination and writes it to the blackboard
 **/
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

        Vector3 spawnPos = new Vector3(blackboard.GetActionDestination().x, blackboard.GetActionDestination().y, blackboard.GetActionDestination().z);

        while (tries > 0)
        {
            float radius = 1.0f;

            if (Physics.CheckSphere(spawnPos, radius))
            {
                float x = blackboard.GetActionDestination().x + Random.Range(0f, 1f);
                float z = blackboard.GetActionDestination().z + Random.Range(0f, 1f);

                spawnPos.x = x;
                spawnPos.z = z;

                tries--;
            }
            else
            {
                blackboard.SetActionDestination(spawnPos);
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