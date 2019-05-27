using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Building : PlayerSelectableObject
{
    protected string prefabPath;
    protected GameObject instantiatedObject;
    protected Queue<UnitCommand> unitCommandQueue = new Queue<UnitCommand>();
    protected Coroutine unitCommandCoroutine;
    protected ContextProvider context;

    public Building(ContextProvider context)
    {
        this.context = context;
    }

    public void CreatePlacebleModel()
    {
        instantiatedObject = Object.Instantiate(Resources.Load<GameObject>(prefabPath));
    }

    public Vector3 GetUnitCreationPosition()
    {
        //TODO later the player may reposition this
        return instantiatedObject.transform.position + new Vector3(8, 0, 0);
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
        unitCommandQueue.Enqueue(command);
        ExecuteNextUnitCommand();
    }

    protected void ExecuteNextUnitCommand()
    {
        if(unitCommandQueue.Count > 0 && unitCommandCoroutine == null) {
            UnitCommand next = unitCommandQueue.Dequeue();
            next.SetOnDoneListener(new OnUnitCommandDone(this));
            unitCommandCoroutine = context.GetMonoBehaviour().StartCoroutine(next.Execute());
        }
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
            parent.unitCommandCoroutine = null;
            parent.ExecuteNextUnitCommand();
        }
    }

    public virtual void OnPlaced()
    {
        //nothing here, yet
    }

    public void Select(bool selected)
    {
        Debug.Log("Building selected: " + selected);
    }

    public GameObject GetGameObject()
    {
        return instantiatedObject;
    }
}
