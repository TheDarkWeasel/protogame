using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public abstract class Unit
{
    protected string prefabPath;
    protected GameObject instantiatedObject;

    public void CreatePlacebleModel()
    {
        instantiatedObject = Object.Instantiate(Resources.Load<GameObject>(prefabPath));
        instantiatedObject.AddComponent<Rigidbody>();
        instantiatedObject.AddComponent<BoxCollider>();
        instantiatedObject.AddComponent<NavMeshAgent>();
    }

    public void SetPosition(Vector3 position)
    {
        instantiatedObject.transform.position = position;
    }

    public void MoveToPosition(Vector3 position)
    {
        instantiatedObject.GetComponent<NavMeshAgent>().SetDestination(position);
    }

    public void Destroy()
    {
        Object.Destroy(instantiatedObject);
    }
}
