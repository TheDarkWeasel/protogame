using UnityEngine;
using System.Collections;

public class UnitMicroAI : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveTo(Vector3 target)
    {
        //TODO
        IBlackboard blackboard = null;
        createMovementBehaviorTree(blackboard);
    }

    private Node2 createMovementBehaviorTree(IBlackboard blackboard)
    {
        Node2 moveSequence = new Sequence(blackboard);
        //TODO
        return null;
    }
}
