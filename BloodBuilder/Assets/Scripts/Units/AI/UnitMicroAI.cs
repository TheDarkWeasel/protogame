using UnityEngine;
using System.Collections;

public class UnitMicroAI : MonoBehaviour
{
    Node2 currentBehaviorTree = null;

    void Update()
    {
        if (currentBehaviorTree != null)
        {
            if(currentBehaviorTree.CheckConditions())
            {
                currentBehaviorTree.DoAction();
            }
        }
    }

    public void MoveTo(Vector3 target)
    {
        //TODO
        IBlackboard blackboard = null;
        currentBehaviorTree = CreateMovementBehaviorTree(blackboard);
    }

    private Node2 CreateMovementBehaviorTree(IBlackboard blackboard)
    {
        //We don't really have a tree here, only a sequence leaf ;)
        Sequence moveSequence = new Sequence(blackboard);

        moveSequence.Add(new CalculateNextFreePositionNode(blackboard));
        moveSequence.Add(new MoveToDestinationNode(blackboard));
        moveSequence.Add(new WaitUntilArrivedAtDestinationNode(blackboard));

        return moveSequence;
    }
}
