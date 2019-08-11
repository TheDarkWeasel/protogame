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
        return blackboard.GetTargetGameObject().activeSelf && blackboard.GetActionDestination() != null;
    }

    public override void DoAction()
    {
        int tries = 100;

        //add 0.1 to y to ignore ground
        Vector3 spawnPos = new Vector3(blackboard.GetActionDestination().x, blackboard.GetActionDestination().y + 0.1f , blackboard.GetActionDestination().z);

        while (tries > 0)
        {
            float radius = 1.0f;

            if (Physics.CheckBox(spawnPos, new Vector3(radius, 0, radius)))
            {
                float x = blackboard.GetActionDestination().x + Random.Range(0f, 1f);
                float z = blackboard.GetActionDestination().z + Random.Range(0f, 1f);

                spawnPos.x = x;
                spawnPos.z = z;

                tries--;
            }
            else
            {
                //reset y before writing to blackboard
                spawnPos.y = blackboard.GetActionDestination().y;
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