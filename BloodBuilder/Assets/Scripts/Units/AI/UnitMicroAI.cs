using UnityEngine;

/**
 * Micro AI for Units. Decides what to do using behavior trees.
 **/
public class UnitMicroAI : MonoBehaviour
{
    Node2 currentBehaviorTree = null;
    UnitNavigation unitNavigation = null;

    void Awake()
    {
        if (unitNavigation == null)
        {
            gameObject.AddComponent<UnitNavigation>();
            unitNavigation = gameObject.GetComponent<UnitNavigation>();
        }
    }

    void Update()
    {
        if (currentBehaviorTree != null)
        {
            if (currentBehaviorTree.CheckConditions())
            {

                currentBehaviorTree.DoAction();
            }

            if (currentBehaviorTree.GetControl().Finished())
            {
                currentBehaviorTree = null;
            }
        }
    }

    public void MoveTo(Vector3 target)
    {
        IBlackboard currentBlackboard = new DefaultBlackboard();
        currentBlackboard.SetActionDestination(target);
        currentBlackboard.SetTargetGameObject(gameObject);
        currentBehaviorTree = CreateMovementBehaviorTree(currentBlackboard);
    }

    private Node2 CreateMovementBehaviorTree(IBlackboard blackboard)
    {
        //We don't really have a tree here, only a sequence leaf ;)
        Sequence moveSequence = new Sequence(blackboard);

        moveSequence.Add(new CalculateNextFreePositionNode(blackboard));
        moveSequence.Add(new MoveToDestinationNode(blackboard));
        moveSequence.Add(new WaitUntilArrivedAtDestinationNode(blackboard));

        moveSequence.Start();

        return moveSequence;
    }
}
