using UnityEngine;

/**
 * Micro AI for Units. Decides what to do using behavior trees.
 **/
public class UnitMicroAI : MonoBehaviour
{
    Node2 currentBehaviorTree = null;

    void Update()
    {
        if (currentBehaviorTree != null)
        {
            if (currentBehaviorTree.CheckConditions())
            {
                currentBehaviorTree.DoAction();
            }
        }
    }

    public void MoveTo(Vector3 target)
    {
        IBlackboard blackboard = new DefaultBlackboard();
        blackboard.SetActionDestination(target);
        blackboard.SetTargetGameObject(gameObject);
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
