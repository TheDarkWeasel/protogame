using UnityEngine;
using System.Collections.Generic;

public abstract class Unit : ISacrificableSelectableObject
{
    protected string prefabPath;
    protected GameObject instantiatedObject;
    protected GameObject selectionCircle;
    protected bool selected = false;
    protected IUnitManager parentManager;
    protected UnitBuildChoiceProvider unitBuildChoiceProvider;

    public IUnitManager ParentManager { get => parentManager; set => parentManager = value; }
    public UnitBuildChoiceProvider UnitBuildChoiceProvider { get => unitBuildChoiceProvider; set => unitBuildChoiceProvider = value; }

    public Unit(IUnitManager parentManager, UnitBuildChoiceProvider unitBuildChoiceProvider)
    {
        this.parentManager = parentManager;
        this.unitBuildChoiceProvider = unitBuildChoiceProvider;
    }

    public void CreatePlacebleModel()
    {
        instantiatedObject = Object.Instantiate(Resources.Load<GameObject>(prefabPath));
        instantiatedObject.AddComponent<Rigidbody>();
        instantiatedObject.AddComponent<BoxCollider>();
        instantiatedObject.AddComponent<UnitMicroAI>();
    }

    public void SetPosition(Vector3 position)
    {
        instantiatedObject.transform.position = position;
    }

    public void MoveToPosition(Vector3 position)
    {
        instantiatedObject.GetComponent<UnitMicroAI>().MoveTo(position);
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
    }

    public void DestroySelectionCircle()
    {
        if (GetSelectionCircle() != null)
        {
            Object.Destroy(GetSelectionCircle().gameObject);
            SetSelectionCircle(null);
        }
    }

    public void CreateSelectionCircle(GameObject selectionCirclePrefab)
    {
        if (GetSelectionCircle() == null)
        {
            SetSelectionCircle(Object.Instantiate(selectionCirclePrefab));
            GetSelectionCircle().transform.SetParent(GetGameObject().transform, false);
            GetSelectionCircle().transform.eulerAngles = new Vector3(90, 0, 0);
            GetSelectionCircle().GetComponent<Projector>().orthographicSize = GetOrthographicSizeForSelectionCircle();
        }
    }

    public int GetSelectionPriority()
    {
        return 0;
    }

    public List<BuildChoice> GetBuildChoices()
    {
        return unitBuildChoiceProvider.GetBuildChoicesForSelectedBlood();
    }
}
