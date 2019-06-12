using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public abstract class Building : PlayerSelectableObject
{
    protected string prefabPath;
    protected GameObject instantiatedObject;
    protected Queue<UnitCommand> unitCommandQueue = new Queue<UnitCommand>();
    protected Coroutine unitCommandCoroutine;
    protected ContextProvider context;
    protected GameObject selectionCircle;
    protected bool selected = false;

    public Building(ContextProvider context)
    {
        this.context = context;
    }

    public void CreatePlacebleModel()
    {
        instantiatedObject = Object.Instantiate(Resources.Load<GameObject>(prefabPath));
    }

    /**
     * Position at which the unit is initially created before being moved to the assembly point
     * */
    public Vector3 GetUnitCreationPosition()
    {
        return instantiatedObject.transform.position + new Vector3(8, 0.001f, 0);
    }

    /**
    * Position at which the produced units gather
    * */
    public Vector3 GetUnitAssemblyPoint()
    {
        //TODO later the player may reposition this
        return instantiatedObject.transform.position + new Vector3(16, 0.001f, 0);
    }

    public void SetPosition(Vector3 position)
    {
        instantiatedObject.transform.position = position;
    }

    public void Destroy()
    {
        Object.Destroy(instantiatedObject);
    }

    public void AddUnitCommand(UnitCommand command)
    {
        Debug.Log("Unit command added");
        unitCommandQueue.Enqueue(command);
        ExecuteNextUnitCommand();
    }

    protected void ExecuteNextUnitCommand()
    {

        if(unitCommandQueue.Count > 0 && unitCommandCoroutine == null) {
            Debug.Log("Executing next command");
            UnitCommand next = unitCommandQueue.Dequeue();
            next.SetOnDoneListener(new OnUnitCommandDone(this));
            unitCommandCoroutine = context.GetMonoBehaviour().StartCoroutine(next.Execute());
        }
    }

    void ResetCommandCoroutine()
    {
        unitCommandCoroutine = null;
    }

    /**
     * Why do I need to to write so much code for a simple listener?
     **/
    private class OnUnitCommandDone : OnDone
    {
        private Building parent;

        public OnUnitCommandDone(Building parent)
        {
            this.parent = parent;
        }

        public void run()
        {
            parent.ResetCommandCoroutine();
            parent.ExecuteNextUnitCommand();
        }
    }

    public virtual void OnPlaced()
    {
        instantiatedObject.AddComponent<BoxCollider>();
        instantiatedObject.AddComponent<NavMeshObstacle>();
    }

    public virtual void Update()
    {
        //nothing here, yet
    }

    public void Select(bool selected)
    {
        Debug.Log("Building selected: " + selected);
        this.selected = selected;
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

    public bool IsSelected()
    {
        return selected;
    }
}
