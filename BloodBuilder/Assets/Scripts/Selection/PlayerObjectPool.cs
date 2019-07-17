using System.Collections.Generic;

public class PlayerObjectPool
{
    private List<SelectableObjectContainer> selectableObjectcontainers = new List<SelectableObjectContainer>();

    private List<PlayerSelectableObject> playerSelectableObjects = new List<PlayerSelectableObject>();
    private List<PlayerSelectableObject> selectedObjects = new List<PlayerSelectableObject>();
    private List<SacrificableSelectableObject> sacrificableSelectableObjects = new List<SacrificableSelectableObject>();

    public void RegisterSelectableObjectContainer(SelectableObjectContainer obj)
    {
        selectableObjectcontainers.Add(obj);
    }

    public List<PlayerSelectableObject> GetPlayerSelectableObjects()
    {
        return GetObjects(playerSelectableObjects, SelectionState.ALL);
    }

    public List<PlayerSelectableObject> GetSelectedObjects()
    {
        return GetObjects(selectedObjects, SelectionState.SELECTED);
    }

    private List<PlayerSelectableObject> GetObjects(List<PlayerSelectableObject> outList, SelectionState state)
    {
        outList.Clear();
        foreach (SelectableObjectContainer selectableObjectContainer in selectableObjectcontainers)
        {
            selectableObjectContainer.GetPlayerSelectableObjects(outList, state);
        }
        return outList;
    }

    public List<SacrificableSelectableObject> GetSacrificableSelectedObjects()
    {
        sacrificableSelectableObjects.Clear();
        foreach (SelectableObjectContainer selectableObjectContainer in selectableObjectcontainers)
        {
            selectableObjectContainer.GetSacrificableSelectableObjects(sacrificableSelectableObjects, SelectionState.SELECTED);
        }
        return sacrificableSelectableObjects;
    }
}
