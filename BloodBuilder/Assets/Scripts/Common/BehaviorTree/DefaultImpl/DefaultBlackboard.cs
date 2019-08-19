using System.Collections.Generic;
using UnityEngine;

public class DefaultBlackboard : IBlackboard
{
    private Vector3 actionDestination;
    private GameObject targetObject;
    private List<Vector3> blockedLocations;

    public DefaultBlackboard()
    {
        blockedLocations = new List<Vector3>();
    }

    public Vector3 GetActionDestination()
    {
        return actionDestination;
    }

    public List<Vector3> GetBlockedLocations()
    {
        return blockedLocations;
    }

    public GameObject GetTargetGameObject()
    {
        return targetObject;
    }

    public void SetActionDestination(Vector3 destination)
    {
        actionDestination = destination;
    }

    public void SetBlockedLocations(List<Vector3> blockedLocations)
    {
        this.blockedLocations = blockedLocations;
    }

    public void SetTargetGameObject(GameObject gameObject)
    {
        targetObject = gameObject;
    }
}