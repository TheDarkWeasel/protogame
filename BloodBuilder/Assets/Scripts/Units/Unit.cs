using UnityEngine;
using System.Collections;

public abstract class Unit
{
    protected string prefabPath;
    protected GameObject instantiatedObject;

    public void CreatePlacebleModel()
    {
        instantiatedObject = Object.Instantiate(Resources.Load<GameObject>(prefabPath));
    }

    public void SetPosition(Vector3 position)
    {
        instantiatedObject.transform.position = position;
    }

    public void Destroy()
    {
        Object.Destroy(instantiatedObject);
    }
}
