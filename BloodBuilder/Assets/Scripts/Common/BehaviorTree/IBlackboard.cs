using UnityEngine;
using System.Collections.Generic;

public interface IBlackboard
{
    GameObject GetTargetGameObject();
    void SetTargetGameObject(GameObject gameObject);
    Vector3 GetActionDestination();
    void SetActionDestination(Vector3 destination);
    List<Vector3> GetBlockedLocations();
    void SetBlockedLocations(List<Vector3> blockedLocations);
}