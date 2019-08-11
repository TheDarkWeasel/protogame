using UnityEngine;

/**
 * Finds the next free spot near the action destination and writes it to the blackboard
 **/
public class CalculateNextFreePositionNode : LeafNode
{
    private IBlackboard blackboard;
    //overall tries left in this node
    private int maxTriesLeft = 100;

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
        //add 0.1 to y to ignore ground
        Vector3 spawnPos = new Vector3(blackboard.GetActionDestination().x, blackboard.GetActionDestination().y + 0.1f, blackboard.GetActionDestination().z);

        //tries left in this frame, before trying again in the next frame. We don't want to block the update-loop for too long.
        int triesPerFrameLeft = 20;

        if (maxTriesLeft > 0)
        {
            while (triesPerFrameLeft > 0)
            {
                float radius = 1.0f;

                if (Physics.CheckBox(spawnPos, new Vector3(radius, 0, radius)))
                {
                    float x = spawnPos.x + Random.Range(-0.9f, 0.9f);
                    float z = spawnPos.z + Random.Range(-0.9f, 0.9f);

                    spawnPos.x = x;
                    spawnPos.z = z;

                    maxTriesLeft--;
                    triesPerFrameLeft--;
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
        }

        //After 100 tries this is also a success. We will just work with what we got und let the NavAgent handle the rest.
        if (maxTriesLeft <= 0)
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