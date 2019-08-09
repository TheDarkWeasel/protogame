using System.Collections.Generic;

public class PlayerObjectPool
{
    private List<ISelectableObjectContainer> selectableObjectcontainers = new List<ISelectableObjectContainer>();

    private List<IPlayerSelectableObject> playerSelectableObjects = new List<IPlayerSelectableObject>();
    private List<IPlayerSelectableObject> selectedObjects = new List<IPlayerSelectableObject>();
    private List<ISacrificableSelectableObject> sacrificableSelectableObjects = new List<ISacrificableSelectableObject>();

    public void RegisterSelectableObjectContainer(ISelectableObjectContainer obj)
    {
        selectableObjectcontainers.Add(obj);
    }

    public List<IPlayerSelectableObject> GetPlayerSelectableObjects()
    {
        return GetObjects(playerSelectableObjects, SelectionState.ALL);
    }

    public List<IPlayerSelectableObject> GetSelectedObjects()
    {
        return GetObjects(selectedObjects, SelectionState.SELECTED);
    }

    private List<IPlayerSelectableObject> GetObjects(List<IPlayerSelectableObject> outList, SelectionState state)
    {
        outList.Clear();
        foreach (ISelectableObjectContainer selectableObjectContainer in selectableObjectcontainers)
        {
            selectableObjectContainer.GetPlayerSelectableObjects(outList, state);
        }
        return outList;
    }

    public List<ISacrificableSelectableObject> GetSacrificableSelectedObjects()
    {
        sacrificableSelectableObjects.Clear();
        foreach (ISelectableObjectContainer selectableObjectContainer in selectableObjectcontainers)
        {
            selectableObjectContainer.GetSacrificableSelectableObjects(sacrificableSelectableObjects, SelectionState.SELECTED);
        }
        return sacrificableSelectableObjects;
    }
}
