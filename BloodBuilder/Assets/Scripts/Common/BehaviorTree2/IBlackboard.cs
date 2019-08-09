using UnityEngine;
using UnityEditor;

public interface IBlackboard
{
    GameObject GetTargetGameObject();
    void SetTargetGameObject(GameObject gameObject);
    Vector3 GetActionDestination();
    void SetActionDestination(Vector3 destination);
}