using UnityEngine;
using System.Collections;

public abstract class Unit : SacrificableSelectableObject
{
    protected string prefabPath;
    protected GameObject instantiatedObject;
    protected GameObject selectionCircle;
    protected bool selected = false;
    protected UnitManager parentManager;

    public Unit(UnitManager parentManager)
    {
        this.parentManager = parentManager;
    }

    public void CreatePlacebleModel()
    {
        instantiatedObject = Object.Instantiate(Resources.Load<GameObject>(prefabPath));
        instantiatedObject.AddComponent<Rigidbody>();
        instantiatedObject.AddComponent<BoxCollider>();
        instantiatedObject.AddComponent<UnitNavigation>();
    }

    public void SetPosition(Vector3 position)
    {
        instantiatedObject.transform.position = position;
    }

    public void MoveToPosition(Vector3 position)
    {
        instantiatedObject.GetComponent<UnitNavigation>().SetDestination(position);
    }

    public void Destroy()
    {
        Object.Destroy(instantiatedObject);
    }

    public void Select(bool selected)
    {
        if (this.selected != selected)
        {
            if (selected)
            {
                PlayerResources.GetInstance().IncreaseResource(GetBloodAmount(), PlayerResources.PlayerResource.SELECTED_BLOOD);
            }
            else
            {
                PlayerResources.GetInstance().DecreaseResource(GetBloodAmount(), PlayerResources.PlayerResource.SELECTED_BLOOD);
            }
            this.selected = selected;
        }
    }

    public bool IsSelected()
    {
        return this.selected;
    }

    public virtual void Update()
    {
        //Nothing to do here, yet
    }

    public GameObject GetGameObject()
    {
        return instantiatedObject;
    }

    public GameObject GetSelectionCircle()
    {
        return selectionCircle;
    }

    public void SetSelectionCircle(GameObject selectionCircle)
    {
        this.selectionCircle = selectionCircle;
    }

    public float GetOrthographicSizeForSelectionCircle()
    {
        //may be overriden, when we have more units
        return 1.5f;
    }

    public abstract int GetBloodAmount();

    public void Sacrifice()
    {
        parentManager.ReleaseUnit(this);
        parentManager = null;
    }
}
