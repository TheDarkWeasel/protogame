using UnityEngine;

public class DefaultBlackboard : IBlackboard
{
    private Vector3 actionDestination;
    private GameObject targetObject;

    public DefaultBlackboard()
    {
    }

    public Vector3 GetActionDestination()
    {
        return actionDestination;
    }

    public GameObject GetTargetGameObject()
    {
        return targetObject;
    }

    public void SetActionDestination(Vector3 destination)
    {
        actionDestination = destination;
    }

    public void SetTargetGameObject(GameObject gameObject)
    {
        targetObject = gameObject;
    }
}